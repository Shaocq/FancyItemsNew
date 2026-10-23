using Duckov.Modding;
using Duckov.UI;
using Duckov.Utilities;
using HarmonyLib;
using ItemStatsSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

namespace FancyItems.Patches
{
    /// <summary>
    /// ItemDisplay 相关的 Harmony 补丁
    /// </summary>
    public static class ItemDisplayPatches
    {
        [HarmonyPatch(typeof(ItemDisplay), "OnEnable")]
        public static class ItemDisplayOnEnablePatch
        {
            static void Postfix(ItemDisplay __instance)
            {
                // 始终挂载：音效和视觉是两个独立开关，由组件内部按当前配置动态判断，
                // 这样运行中切换任一开关都能即时生效，不会出现“关掉视觉就没了音效”。
                if (__instance != null && __instance.gameObject != null &&
                    __instance.GetComponent<Systems.UI.ItemQualityVisualizer>() == null)
                {
                    __instance.gameObject.AddComponent<Systems.UI.ItemQualityVisualizer>();
                }
            }
        }

        public static int ProcessExistingItemDisplays()
        {
            ItemDisplay[] displays = Object.FindObjectsOfType<ItemDisplay>();
            int processed = 0;

            foreach (ItemDisplay display in displays)
            {
                if (display == null) continue;

                if (display.GetComponent<Systems.UI.ItemQualityVisualizer>() == null)
                {
                    display.gameObject.AddComponent<Systems.UI.ItemQualityVisualizer>();
                    processed++;
                }
            }
            return processed;
        }

        public static int CleanupAllQualityVisualizers()
        {
            int cleanedCount = 0;

            // 清理视觉效果组件（含未激活物品格上的组件，避免停用 Mod 后残留）
            Systems.UI.ItemQualityVisualizer[] visualizers =
                Resources.FindObjectsOfTypeAll<Systems.UI.ItemQualityVisualizer>();
            foreach (var visualizer in visualizers)
            {
                if (visualizer == null) continue;
                Object.Destroy(visualizer);
                cleanedCount++;
            }

            return cleanedCount;
        }
    }

    /// <summary>
    /// 搜索时间优化相关的 Harmony 补丁
    /// </summary>
    public static class LootTimePatches
    {
        [HarmonyPatch(typeof(GameplayDataSettings.LootingData), "MGetInspectingTime")]
        public static class LootingDataMGetInspectingTimePatch
        {
            static void Postfix(Item item, ref float __result)
            {
                if (!Core.ModConfiguration.EnableSearchOptimization) return;

                // 在原始方法执行后，获取原始结果
                float originalTime = __result;

                // 使用我们的优化时间计算
                float optimizedTime = Systems.Optimization.SearchTimeOptimizer.GetOptimizedInspectingTime(item);

                // 如果返回-1，表示保持原始时间，不需要做任何修改
                if (optimizedTime < 0)
                {
                    // __result已经是原始时间，保持不变
                    return;
                }

                // 记录对比信息并应用优化 - 只记录被优化的品质(0、1、2)
                if (item != null && item.Quality <= 2)
                {
                    string itemName = item.DisplayName ?? "Unknown";
                    float reductionPercent = (originalTime > 0) ?
                        ((originalTime - optimizedTime) / originalTime * 100f) : 0f;

                    Debug.Log($"{Constants.FancyItemsConstants.LogPrefix} 时间优化: {itemName} (品质{item.Quality}) " +
                             $"{originalTime:F1}s → {optimizedTime:F1}s " +
                             $"(减少{reductionPercent:F0}%)");
                }

                // 只有被优化的物品才应用新时间
                __result = optimizedTime;
            }
        }
    }
}