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

namespace FancyItems.Constants
{
    /// <summary>
    /// FancyItems Mod 全局常量定义
    /// </summary>
    public static class FancyItemsConstants
    {
        /// <summary>
        /// Harmony ID，用于补丁管理
        /// </summary>
        public const string HarmonyId = "com.fancyitems.mod";

        /// <summary>
        /// Mod日志前缀
        /// </summary>
        public const string LogPrefix = "[FancyItems]";

        /// <summary>
        /// 品质背景圆角半径（像素）
        /// </summary>
        public const float BackgroundCornerRadius = 15f;

        /// <summary>
        /// 性能优化更新间隔（秒）
        /// </summary>
        public const float PerformanceUpdateInterval = 0.1f;

        /// <summary>
        /// Boss 专属枪械的 TypeID 白名单。
        /// 这些枪械在原版数据里的 quality 是 9 / 999 等特殊段（不在 2~8 区间），
        /// 因此本来拿不到背景色；这里统一按 5 级（橙色）渲染。
        /// 数据来源：ItemAssetsCollection 实测导出，完整清单见仓库根 docs_boss_guns.md
        /// </summary>
        public static readonly HashSet<int> BossGunItemIds = new HashSet<int>
        {
            914,   // 咕噜枪      PST_StormBoss_Poison
            915,   // 风暴枪      BR_Storm_Space
            916,   // 啪啦枪      MG_StormBoss_Fire
            917,   // 比利枪      SHT_StormBoss_Elec
            437,   // VPO-劳登改  Sniper_VPO_Deng
            1260,  // 糖果枪      SHT_Candy_Ghost
            1264,  // 灵能枪      SHT_TOZ106_Ghost
            1353,  // BSG-I       SHT_SnowBoss_Ice
            1354,  // BSG-F       SHT_SnowBoss_Fire
            1396,  // 纳米枪      NM_NMGun_Elec
            1432,  // Saiga-12K Tagilla改   SHT_Saiga12_TagillaB
            1433,  // AK-74U Tagilla改      SMG_AK74_Tagilla
            1480,  // TS-128智能型狙击枪    SNP_TecSniper_Elec
            1561,  // 死与舞      PST_MingChao_Carletta
            1562,  // 溢彩荧辉    PST_MingChao_Lynae
            1563,  // （无本地化名）SNP_MingChao_Carletta
        };

        /// <summary>
        /// Boss 枪械统一使用的品质档位（5 = 橙色）
        /// </summary>
        public const int BossGunQuality = 5;

        /// <summary>
        /// 品质颜色配置（RGBA）
        /// </summary>
        public static readonly Color[] QualityColors = new Color[]
        {
            new Color(0f, 0f, 0f, 0f),              // Quality 0: 透明
            new Color(0f, 0f, 0f, 0f),              // Quality 1: 透明（普通物品不显示）
            new Color(0.6f, 0.9f, 0.6f, 0.24f),     // Quality 2: 柔和浅绿
            new Color(0.6f, 0.8f, 1.0f, 0.30f),     // Quality 3: 天蓝浅色
            new Color(1.0f, 0.50f, 1.0f, 0.40f),   // Quality 4: 亮浅紫（提亮，略粉）
            new Color(1.0f, 0.75f, 0.2f, 0.60f),   // Quality 5: 柔亮橙（更偏橙、更暖）
            new Color(1.0f, 0.3f, 0.3f, 0.4f),     // Quality 6+: 明亮红（亮度提升、透明度降低）
        };
    }
}