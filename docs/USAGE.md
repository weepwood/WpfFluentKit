# WpfFluentKit 使用指南

## 运行 Gallery

需要 Windows 10/11、.NET 8 SDK（本地编译）、Visual Studio 2022 或 CLI：

```powershell
dotnet restore samples/WpfFluentKit.Gallery/WpfFluentKit.Gallery.csproj
dotnet run --project samples/WpfFluentKit.Gallery/WpfFluentKit.Gallery.csproj
```

GitHub Actions 会构建自包含 win-x64 测试版，在 Actions 对应运行的 Artifacts 中下载 `WpfFluentKit-Gallery-win-x64` 解压运行。

## 接入已有 WPF 应用

1. 添加项目引用 `src/WpfFluentKit/WpfFluentKit.csproj`。
2. 参考示例 `App.xaml` 合并 WPF UI、Violeta、`Palette.xaml` 和 `Controls.xaml`。
3. 使用 `{StaticResource KitCard}`、`{StaticResource KitPageTitle}` 等样式。
4. 使用 `ThemeManager.Apply(isDark, accentColor)` 更新主题。

## 范围与限制

- 此版是可操作的 UI Gallery，任务操作仅切换演示状态，不执行后台任务。
- 组件库提供配色令牌、卡片、侧栏按钮、标题和主题管理；Gallery 中其他 WPF UI 控件仍直接来自上游库。
- 本项目不包含 BetterGI 的 GPL-3.0 代码；上游依赖许可证需依照其项目声明保留。
- 需在 Windows 运行，GitHub Actions 负责可复现构建。