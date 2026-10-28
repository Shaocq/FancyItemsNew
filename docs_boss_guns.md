# Boss / 特殊品质枪械清单

数据来源：`ItemAssetsCollection`（`resources.assets`，path_id `76722`）实测导出。

背景：mod 只给 `quality` 2~8 的物品上背景色；下列枪械的 `quality` 是 **9 / 99 / 999**，默认完全没有背景色。

**当前实现**：`QualityColorConfig.ResolveQuality` 对 `quality > 8` 且带 `Gun` 标签、不带 `DestroyOnLootBox` 的物品统一返回 5（橙色）。也就是下面第一、二组共 **54 件**都已按橙色渲染，无需再逐个登记 TypeID。

## 一、明确带 Boss 关键字的枪械（16 件）

| TypeID | quality | 内部名 | 中文名 |
| --- | --- | --- | --- |
| 437 | 9 | Sniper_VPO_Deng | VPO-劳登改 |
| 914 | 9 | PST_StormBoss_Poison | 咕噜枪 |
| 915 | 9 | BR_Storm_Space | 风暴枪 |
| 916 | 9 | MG_StormBoss_Fire | 啪啦枪 |
| 917 | 9 | SHT_StormBoss_Elec | 比利枪 |
| 1260 | 9 | SHT_Candy_Ghost | 糖果枪 |
| 1264 | 9 | SHT_TOZ106_Ghost | 灵能枪 |
| 1353 | 9 | SHT_SnowBoss_Ice | BSG-I |
| 1354 | 9 | SHT_SnowBoss_Fire | BSG-F |
| 1396 | 9 | NM_NMGun_Elec | 纳米枪 |
| 1432 | 9 | SHT_Saiga12_TagillaB | Saiga-12K  Tagilla改 |
| 1433 | 9 | SMG_AK74_Tagilla | AK-74U  Tagilla改 |
| 1480 | 9 | SNP_TecSniper_Elec | TS-128智能型狙击枪 |
| 1561 | 9 | PST_MingChao_Carletta | 死与舞 |
| 1562 | 9 | PST_MingChao_Lynae | 溢彩荧辉 |
| 1563 | 9 | SNP_MingChao_Carletta | (无本地化名) |

## 二、其余特殊品质枪械（38 件）

| TypeID | quality | 内部名 | 中文名 |
| --- | --- | --- | --- |
| 4 | 9 | 1_Rifle-A_template | GunTemplate |
| 302 | 9 | 1_EnemyGun_Template | 枪 |
| 391 | 9 | Energy_CubeGun | 方块枪 |
| 733 | 9 | SMG_MP7_Electric | 带电MP7 |
| 862 | 9 | AR_AK47_Fire | 带火AK-47 |
| 943 | 9 | Poop_ShitGun_Poison | 噗噗枪 |
| 946 | 9 | Poop_ShotShitGun_Poison | 噗噗喷 |
| 1055 | 999 | AR_AK47_Quest | 特制AK47 |
| 1056 | 999 | AR_AUGA3_Quest | 特制StG 77A3 |
| 1057 | 999 | BR_M14_Quest | 特制M14 |
| 1058 | 999 | PST_Glick_Quest | 特制格力克 |
| 1059 | 999 | SHT_MP155_Quest | 特制MP-155 |
| 1060 | 999 | SMG_SR3M_Quest | 特制SR-3M |
| 1061 | 999 | Sniper_Mosin_Quest | 特制Mosin-Nagant |
| 1089 | 9 | SHT_TOZ66_Fire | TOZ-66龙息 |
| 1209 | 9 | SMG_ArcadeGun_Normal | 玩具枪 |
| 1238 | 9 | AR_MF_Poison | MF-毒液 |
| 1289 | 9 | ARR_CrossBow_Normal | 十字弩 |
| 1298 | 9 | ARR_AHBow_Ice | 冰原反曲弓 |
| 1299 | 9 | ARR_AHBow_Normal | 反曲弓 |
| 1300 | 9 | AR_RPD_Ice | RPD-凛冬 |
| 1301 | 9 | RKT_SnowGun_Ice | 雪球枪 |
| 1302 | 9 | SMG_SR3M_Ice | 霜冻SR-3M |
| 1362 | 9 | AR_MMG_Ice | MMG-冰锋 |
| 1386 | 9 | SHT_AA12_Normal | AA-12 |
| 1431 | 9 | SHT_Saiga12_Normal | Saiga-12K |
| 1521 | 9 | RKT_FireWork_Fire | 烟花枪 |
| 1526 | 99 | Plant_AR_Normal | (无本地化名) |
| 1528 | 99 | Plant_BR_Normal | (无本地化名) |
| 1529 | 99 | Plant_PST_Normal | (无本地化名) |
| 1530 | 99 | Plant_SHT_Fire | (无本地化名) |
| 1531 | 99 | Plant_SHT_Normal | (无本地化名) |
| 1532 | 99 | Plant_SMG_Normal | (无本地化名) |
| 1547 | 99 | SNP_GunFire_Normal | (无本地化名) |
| 1548 | 99 | SMG_GunFire_Normal | (无本地化名) |
| 1627 | 9 | AR_QBZ191_Normal | QBZ-191 |
| 1628 | 9 | RKT_WaterGun_Ice | 水球枪 |
| 1629 | 9 | SMG_MPX_Normal | MPX |

## 三、敌人专用、拾取即销毁（带 `DestroyOnLootBox`，22 件，不建议渲染）

| TypeID | quality | 内部名 | 中文名 |
| --- | --- | --- | --- |
| 303 | 9 | EGun_SHOT_Storm_NoGraphic | (无本地化名) |
| 304 | 9 | EGun_AR_Storm_NoGraphic | (无本地化名) |
| 310 | 9 | EGun_Magic | (无本地化名) |
| 311 | 9 | EGun_SMG_Storm_NoGraphic | (无本地化名) |
| 341 | 9 | EnemyGun_Spider_Rifle | (无本地化名) |
| 342 | 9 | EnemyGun_Spider_Ring | (无本地化名) |
| 879 | 9 | EGun_Magic_Ring | (无本地化名) |
| 892 | 9 | EnemyGun_RoadBlock_Rifle | (无本地化名) |
| 898 | 9 | EGun_StormBoss_1_BreakArmor | (无本地化名) |
| 899 | 9 | EGun_StormBoss_2_Poison | (无本地化名) |
| 900 | 9 | EGun_StormBoss_3_Fire | (无本地化名) |
| 901 | 9 | EGun_StormBoss_4_Electric | (无本地化名) |
| 902 | 9 | EGun_StormBoss_5_Space | (无本地化名) |
| 931 | 9 | EGun_Magic_Ring_Mushroom | (无本地化名) |
| 1239 | 9 | EGun_Magic_Red | (无本地化名) |
| 1258 | 9 | EGun_AR_Storm_Virus | (无本地化名) |
| 1374 | 9 | EGun_Turret_MG | 机枪炮台 |
| 1462 | 9 | EGun_SnowGun_Ice | (无本地化名) |
| 1508 | 9 | EGun_Magic_Blue | (无本地化名) |
| 1518 | 9 | EGun_Magic_Ring_WhiteHorse | (无本地化名) |
| 1533 | 9 | EGun_AR_Zombie | (无本地化名) |
| 1571 | 9 | VehicleGun_Spider_Rifle | (无本地化名) |
