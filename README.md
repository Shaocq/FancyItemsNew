# Fancy Items - 炫彩物品品质显示

[![游戏](https://img.shields.io/badge/游戏-Escape%20from%20Duckov-blue)](https://store.steampowered.com/app/3167020/)
[![协议](https://img.shields.io/badge/协议-MIT-yellow)](LICENSE)
[![版本](https://img.shields.io/badge/版本-1.0.1-brightgreen)](CHANGELOG.md)

> 按物品稀有度给物品格子染上不同背景色，并在搜索完成时播放对应品质的音效。按 F8 打开设置。

| 项目 | 信息 |
| --- | --- |
| 游戏 | 逃离鸭科夫（Escape from Duckov，AppID `3167020`） |
| 框架 | `Duckov.Modding.ModBehaviour` |
| 前置 | 无（自带配置界面） |
| 创意工坊 | — |
| 当前版本 | 1.0.1 |
| 开源协议 | [MIT](LICENSE) |

## 安装

把 `FancyItemsNew` 文件夹放入 `Duckov_Data\Mods\`。游戏内按 `F8` 打开设置界面。

> **`lib/0Harmony.dll` 不在仓库内。** 需要从源码编译时，请把创意工坊前置 HarmonyLib 目录下的 `0Harmony.dll` 复制到 `lib/` 下。


[Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=3588329796)

为《逃离鸭科夫》中的所有物品图标添加品质背景色，让你一眼识别物品稀有度！还有搜索音效~

## ✨ 功能特性

- 🎨 视觉增强：所有物品图标根据品质显示彩色背景（背包、装备栏、地面等）
- 🎵 音效反馈：搜索物品时根据品质播放不同音效
- ⚡ 低级物品加速：降低低品质物品的搜索时间，提升游戏流畅度
- 🛠️ 配置系统：内置完整的设置面板，支持可视化配置，可独立控制各种功能开关和参数

## 📥 安装方法

> 本 Mod 需要前置 **HarmonyLib**（Steam 工坊 ID `3589088839`），请先订阅并启用。

### 方式一：通过 Steam 创意工坊下载安装（推荐）

前往 [Steam 创意工坊](https://steamcommunity.com/sharedfiles/filedetails/?id=3588329796)，或搜索Fancy Items，点击订阅，即可下载最新版本。

### 方式二：通过 Release 下载安装

该方式适用于在非Steam平台下游玩的情况。

1. 前往 [Releases 页面](https://github.com/shiquda/duckov-fancy-items/releases) 下载最新版本
2. 下载 `FancyItems-vX.X.X.zip` 文件
3. 解压到游戏目录：`[游戏目录]/Duckov_Data/Mods/FancyItems/`
4. 启动游戏，并在Mod菜单中启用 "Fancy Items"

### 方式三：自行编译安装（开发者）

1. 配置dotnet开发环境
2. 运行 `build.bat` 构建mod
3. 将 `Release/FancyItems` 目录解压到游戏目录：`[游戏目录]/Duckov_Data/Mods/FancyItems/`
4. 启动游戏，在Mod菜单中启用 "Fancy Items"

> 💡 **提示**：游戏目录通常位于 `XXX\Steam\steamapps\common\Escape from Duckov\`

## 🎮 效果说明

| Quality | 视觉效果 |
|---------|---------|
| 0-1     | 无背景（保持原版），播放普通点击音效 |
| 2       | 🟢 绿色背景 |
| 3       | 🔵 蓝色背景 |
| 4       | 🟣 紫色背景 |
| 5       | 🟠 金色背景 |
| 6-8     | 🔴 红色背景 |
| 9 及以上 | 无背景、无额外音效（特殊分类编码，保持原版） |

> 背景只对品质 2~8 的物品生效；音效对品质 0~8 生效（0/1 播放普通点击音）。
> 品质 9 及以上的特殊分类编码（鱼饵、蔬菜、蓝图等）完全保持原版颜色与音效。

**Boss 专属枪械例外**：这类枪械在原版数据里的品质是 9 / 999 等特殊段，现已按 **5 级（橙色）**统一渲染背景并播放对应音效。
覆盖 16 件：VPO-劳登改、咕噜枪、风暴枪、啪啦枪、比利枪、糖果枪、灵能枪、BSG-I、BSG-F、纳米枪、Saiga-12K Tagilla改、AK-74U Tagilla改、TS-128智能型狙击枪、死与舞、溢彩荧辉、SNP_MingChao_Carletta。
完整调查清单见 [docs_boss_guns.md](docs_boss_guns.md)。

## ⚙️ 设置选项

可按下 `F8` 通过设置面板可以调整以下选项：

- 🎨 视觉增强：所有物品图标根据品质显示彩色背景（背包、装备栏、地面等）
- 🎵 音效反馈：搜索物品时根据品质播放不同音效
- ⚡ 低级物品加速：降低低品质物品的搜索时间，提升游戏流畅度

## ❓ 常见问题

**Q: 看不到彩色背景？**
A:

- 确保Mod已启用
- 普通物品（Quality 1）不显示背景，保持原有透明状态
- 按下 F8，检查设置中是否开启了品质背景功能

**Q: 听不到音效？**
A:

- 音效只在物品品质搜索完成时播放
- 检查设置中是否开启了音效系统
- 确保游戏音效音量不是静音

**Q: 与其他Mod兼容吗？**
A:

本 Mod 使用 Harmony 修改物品格的显示，并额外提供可选的低级物品搜索加速（可在 F8 设置中关闭）。如与其它修改物品格的 Mod 冲突，可先关闭相应功能排查

**Q: 设置不生效？**
A:

- 设置会自动保存，重启游戏后保持
- 设置修改后会立即生效，无需重启游戏

> 作者非游戏专业开发者，若发现bug欢迎pr修复，谢谢~

## 更新日志

[CHANGELOG.md](CHANGELOG.md)

## 许可证

MIT License
