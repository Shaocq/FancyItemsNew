using Duckov.Modding;
using HarmonyLib;
using System.Collections;
using UnityEngine;

// 命名空间必须与 info.ini 的 name 一致：
// ModManager 用 Assembly.GetType("<name>.ModBehaviour") 查找入口，改 Info.ini 后这里要同步
namespace FancyItemsNew
{
    /// <summary>
    /// FancyItemsNew Mod 入口类 - 游戏期望的 ModBehaviour 类
    /// </summary>
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        private FancyItems.Core.FancyItemsMod fancyItemsMod;

        private void OnEnable()
        {
            Debug.Log("[FancyItemsNew] ModBehaviour 已启用");

            // 创建并初始化核心Mod组件
            fancyItemsMod = gameObject.AddComponent<FancyItems.Core.FancyItemsMod>();
        }

        private void OnDisable()
        {
            Debug.Log("[FancyItemsNew] ModBehaviour 已禁用");

            if (fancyItemsMod != null)
            {
                // 调用清理方法
                fancyItemsMod.CleanupMod();
                Destroy(fancyItemsMod);
                fancyItemsMod = null;
            }
        }

        private void OnDestroy()
        {
            OnDisable();
        }
    }
}