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

namespace FancyItems.Systems.UI
{
    /// <summary>
    /// 品质颜色配置类
    /// </summary>
    public static class QualityColorConfig
    {
        /// <summary>
        /// 获取指定品质的颜色
        /// </summary>
        /// <param name="quality">物品品质</param>
        /// <returns>对应的颜色</returns>
        public static Color GetQualityColor(int quality)
        {
            if (quality < 0) quality = 0;
            int colorIndex = Mathf.Min(quality, Constants.FancyItemsConstants.QualityColors.Length - 1);
            return Constants.FancyItemsConstants.QualityColors[colorIndex];
        }

        /// <summary>
        /// 解析物品实际用于渲染/音效的品质。
        /// 特殊段枪械（Boss 枪、元素改型、事件枪、任务「特制」枪等）在原版数据里的
        /// quality 是 9 / 99 / 999，本来落在 [2, 8] 之外、拿不到背景色；
        /// 这里把「可拾取的枪械」统一按 5 级（橙色）处理。
        /// 判据：quality 大于 8，带 Gun 标签，且不带 DestroyOnLootBox
        /// （后者是敌人专用、拾取即销毁的枪，玩家看不到）。
        /// 其余物品原样返回。
        /// </summary>
        /// <param name="quality">物品原始 quality</param>
        /// <param name="tags">物品标签集合</param>
        public static int ResolveQuality(int quality, TagCollection tags)
        {
            if (quality > 8 && tags != null && tags.Contains("Gun") && !tags.Contains("DestroyOnLootBox"))
            {
                return Constants.FancyItemsConstants.BossGunQuality;
            }
            return quality;
        }

        /// <summary>
        /// 是否属于视觉处理区间 [2, 8]
        /// 区间外（0/1，以及 9 及以上的特殊分类编码，如 91/999/9999）不渲染背景，
        /// 保持原版颜色表现
        /// </summary>
        public static bool IsInQualityRange(int quality)
        {
            return quality >= 2 && quality <= 8;
        }

        /// <summary>
        /// 是否属于音效处理区间 [0, 8]
        /// 品质 0/1 也要播放音效；9 及以上的特殊分类编码保持原版音效
        /// </summary>
        public static bool IsInSoundRange(int quality)
        {
            return quality >= 0 && quality <= 8;
        }

        /// <summary>
        /// 检查指定品质是否应该显示背景
        /// </summary>
        /// <param name="quality">物品品质</param>
        /// <returns>是否显示背景</returns>
        public static bool ShouldShowBackground(int quality)
        {
            return IsInQualityRange(quality);
        }

        /// <summary>
        /// 获取背景透明度
        /// </summary>
        /// <param name="quality">物品品质</param>
        /// <returns>透明度值</returns>
        public static float GetBackgroundAlpha(int quality)
        {
            return GetQualityColor(quality).a;
        }
    }
}