using Duckov.Modding;
using Duckov.UI;
using Duckov.Utilities;
using FancyItems.Core;
using HarmonyLib;
using ItemStatsSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

namespace FancyItems.Systems.UI
{
    /// <summary>
    /// 设置界面管理器
    /// 负责创建和管理 Mod 的设置界面（使用自动化生成器）
    /// </summary>
    public class SettingsUIManager : MonoBehaviour
    {
        private GameObject settingsPanel;
        private Canvas canvas;
        private RectTransform panelRect;

        private bool isInitialized = false;
        // Canvas 是否由本 mod 创建（自建的 Canvas 需要在清理时一并销毁）
        private bool ownsCanvas = false;

        void Awake()
        {
            // 订阅配置变更事件
            ConfigManager.OnConfigChanged += OnConfigChanged;
        }

        /// <summary>
        /// 显示设置界面
        /// </summary>
        public void ShowSettings()
        {
            // 面板或 Canvas 被销毁（例如切换场景）时允许重建，避免 F8 永久失效
            if (settingsPanel == null || canvas == null)
            {
                isInitialized = false;
            }

            if (!isInitialized)
            {
                InitializeSettingsPanel();
            }

            if (settingsPanel != null)
            {
                settingsPanel.SetActive(true);
                // 放到最上层，避免被游戏 UI 遮住
                settingsPanel.transform.SetAsLastSibling();
                UpdateUIValues();
                Debug.Log($"{Constants.FancyItemsConstants.LogPrefix} 设置界面已显示");
            }
        }

        /// <summary>
        /// 隐藏设置界面
        /// </summary>
        public void HideSettings()
        {
            if (settingsPanel != null)
            {
                settingsPanel.SetActive(false);
                Debug.Log($"{Constants.FancyItemsConstants.LogPrefix} 设置界面已隐藏");
            }
        }

        /// <summary>
        /// 检查设置界面是否可见
        /// </summary>
        public bool IsVisible()
        {
            return settingsPanel != null && settingsPanel.activeSelf;
        }

        /// <summary>
        /// 初始化设置界面
        /// </summary>
        private void InitializeSettingsPanel()
        {
            try
            {
                CreateSettingsPanel();
                CreateUIComponents();
                // 自建 UI 的层级统一跟随 Canvas，避免相机剔除导致不可见
                SetLayerRecursively(settingsPanel, canvas.gameObject.layer);
                BindUIEvents();

                // 验证设置面板已创建
                if (settingsPanel != null)
                {
                    isInitialized = true;
                    Debug.Log($"{Constants.FancyItemsConstants.LogPrefix} 设置界面初始化完成");
                }
                else
                {
                    Debug.LogError($"{Constants.FancyItemsConstants.LogPrefix} 设置面板创建失败");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"{Constants.FancyItemsConstants.LogPrefix} 设置界面初始化失败: {e.Message}");
                Debug.LogError($"{Constants.FancyItemsConstants.LogPrefix} 堆栈跟踪: {e.StackTrace}");
            }
        }

        /// <summary>
        /// 创建设置面板
        /// </summary>
        private void CreateSettingsPanel()
        {
            // 使用自建的独立 Overlay Canvas：
            // 1) 不依赖游戏场景里的 Canvas，切换场景不会被销毁；
            // 2) sortingOrder 拉高后稳定盖在游戏 UI 之上；
            // 3) ScreenSpaceOverlay 不受相机 layer 剔除影响。
            if (canvas == null)
            {
                GameObject canvasObj = new GameObject("FancyItems Settings Canvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 30000;

                CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
                CopyGameCanvasScaler(scaler);

                canvasObj.AddComponent<GraphicRaycaster>();
                canvasObj.layer = 5; // Unity 内置 UI 层
                // mod 本体挂在持久对象下，自建 Canvas 也必须持久化
                Object.DontDestroyOnLoad(canvasObj);
                ownsCanvas = true;
            }

            // 重建前清掉可能残留的旧面板
            if (settingsPanel != null)
            {
                Destroy(settingsPanel);
                settingsPanel = null;
            }

            // 创建设置面板
            settingsPanel = new GameObject("FancyItems Settings Panel", typeof(RectTransform));
            settingsPanel.transform.SetParent(canvas.transform, false);
            settingsPanel.layer = canvas.gameObject.layer;

            // 添加背景面板
            Image backgroundImage = settingsPanel.AddComponent<Image>();
            backgroundImage.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);

            panelRect = settingsPanel.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.3f, 0.2f);
            panelRect.anchorMax = new Vector2(0.7f, 0.8f);
            panelRect.offsetMin = Vector2.zero;
            panelRect.offsetMax = Vector2.zero;

            // 默认隐藏
            settingsPanel.SetActive(false);
        }

        /// <summary>
        /// 复制游戏主 Canvas 的缩放参数，保证设置面板尺寸比例与游戏 UI 一致
        /// </summary>
        private static void CopyGameCanvasScaler(CanvasScaler target)
        {
            CanvasScaler[] scalers = FindObjectsOfType<CanvasScaler>();
            CanvasScaler fallback = null;

            foreach (CanvasScaler source in scalers)
            {
                if (source == null || source.gameObject == target.gameObject) continue;

                Canvas sourceCanvas = source.GetComponent<Canvas>();
                if (sourceCanvas == null || !sourceCanvas.isRootCanvas) continue;

                if (sourceCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
                {
                    ApplyCanvasScaler(source, target);
                    return;
                }

                if (fallback == null) fallback = source;
            }

            if (fallback != null)
            {
                ApplyCanvasScaler(fallback, target);
            }
        }

        private static void ApplyCanvasScaler(CanvasScaler source, CanvasScaler target)
        {
            target.uiScaleMode = source.uiScaleMode;
            target.referenceResolution = source.referenceResolution;
            target.screenMatchMode = source.screenMatchMode;
            target.matchWidthOrHeight = source.matchWidthOrHeight;
            target.scaleFactor = source.scaleFactor;
            target.referencePixelsPerUnit = source.referencePixelsPerUnit;
        }

        /// <summary>
        /// 递归设置层级
        /// </summary>
        private static void SetLayerRecursively(GameObject target, int layer)
        {
            if (target == null) return;
            target.layer = layer;
            foreach (Transform child in target.transform)
            {
                SetLayerRecursively(child.gameObject, layer);
            }
        }

        /// <summary>
        /// 创建 UI 组件（使用自动化生成器）
        /// </summary>
        private void CreateUIComponents()
        {
            // 定义设置项
            var settings = new List<SettingItem>
            {
                new SettingItem("enableSoundEffects", "启用物品品质音效", "开启后检查物品时会播放相应品质的音效", SettingType.Toggle, true, false, "Audio"),
                new SettingItem("enableSearchOptimization", "启用低级物品搜索加速", "开启后低级物品的搜索时间会减少", SettingType.Toggle, true, false, "Optimization"),
                new SettingItem("enableQualityVisuals", "启用物品品质颜色特效", "开启后物品图标会显示对应品质的底色", SettingType.Toggle, true, false, "Visual")
            };

            // 使用自动化生成器创建界面
            AutoSettingsGenerator.GenerateSettingsUI(settingsPanel, settings);
        }

        /// <summary>
        /// 绑定 UI 事件（自动化生成器内部处理）
        /// </summary>
        private void BindUIEvents()
        {
            // 自动化生成器会自动绑定事件
            // 这里可以添加额外的事件处理
        }

        /// <summary>
        /// 更新 UI 值（自动化生成器内部处理）
        /// </summary>
        private void UpdateUIValues()
        {
            // 自动化生成器会自动更新UI值
            // 这里可以添加额外的UI更新逻辑
        }

        /// <summary>
        /// 配置变更事件处理
        /// </summary>
        private void OnConfigChanged(FancyItemsConfig config)
        {
            UpdateUIValues();
        }

        /// <summary>
        /// 清理 UI
        /// </summary>
        private void CleanupUI()
        {
            if (settingsPanel != null)
            {
                // 运行时使用 Destroy，DestroyImmediate 在游戏运行中可能引发异常
                Destroy(settingsPanel);
                settingsPanel = null;
            }

            // 自建 Canvas 是 DontDestroyOnLoad 的，必须手动销毁
            if (ownsCanvas && canvas != null)
            {
                Destroy(canvas.gameObject);
                canvas = null;
                ownsCanvas = false;
            }
        }

        void OnDestroy()
        {
            // 取消订阅配置变更事件
            ConfigManager.OnConfigChanged -= OnConfigChanged;

            // 清理 UI
            CleanupUI();
        }
    }
}