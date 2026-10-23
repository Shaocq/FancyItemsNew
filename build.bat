@echo off
:: Fancy Items Mod 构建脚本
:: 一键构建并准备发布文件

echo [1/4] 清理旧的构建文件...
if exist "Release" (
    rmdir /s /q "Release"
)

echo [2/4] 构建项目...
cd fancy-items
dotnet build --configuration Release

if %ERRORLEVEL% neq 0 (
    echo 构建失败！
    pause
    exit /b 1
)

echo [3/4] 创建发布文件夹...
if not exist "..\Release\FancyItemsNew" mkdir "..\Release\FancyItemsNew"

echo [4/4] 复制文件到发布目录...
copy "bin\Release\netstandard2.1\FancyItemsNewNew.dll" "..\Release\FancyItemsNew\FancyItemsNew.dll" >nul
echo   - FancyItemsNew.dll

:: Harmony 不随 mod 分发，运行时依赖前置「HarmonyLib」（工坊 3589088839）
echo   - 0Harmony.dll 由前置 HarmonyLib 提供，不再打包

copy "info.ini" "..\Release\FancyItemsNew\" >nul
echo   - info.ini

if not exist "preview.png" (
    echo   警告：preview.png 不存在，请手动添加 256x256 的预览图
) else (
    copy "preview.png" "..\Release\FancyItemsNew\" >nul
    echo   - preview.png
)

echo.
echo 发布文件夹内容：
dir "..\Release\FancyItemsNew" /b

echo.
echo 构建完成！安装方法：
echo 1. 将 Release\FancyItemsNew 文件夹复制到：
echo    [游戏目录]\Duckov_Data\Mods\
echo 2. 确保已订阅前置 mod「HarmonyLib」（工坊 3589088839）
echo 3. 启动游戏，在 Mod 菜单中启用 '物品炫彩和搜索音效'
echo 4. 按下 F8 打开设置界面

cd ..
