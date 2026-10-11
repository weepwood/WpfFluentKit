using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfFluentKit;

namespace WpfFluentKit.Gallery;

public partial class MainWindow : Window
{
    private bool _dark = true;
    private Color _accent = Color.FromRgb(102, 119, 238);
    private readonly ObservableCollection<DemoTask> _tasks = new()
    {
        new("导出视频关键帧", "待运行"),
        new("整理本地文件", "待运行"),
        new("生成缩略图", "已完成")
    };

    public MainWindow()
    {
        InitializeComponent();
        TaskList.ItemsSource = _tasks;
        Loaded += (_, _) => ThemeManager.Apply(_dark, _accent);
    }

    private void Navigate(int index)
    {
        OverviewPanel.Visibility = index == 0 ? Visibility.Visible : Visibility.Collapsed;
        ControlsPanel.Visibility = index == 1 ? Visibility.Visible : Visibility.Collapsed;
        TasksPanel.Visibility = index == 2 ? Visibility.Visible : Visibility.Collapsed;
        SettingsPanel.Visibility = index == 3 ? Visibility.Visible : Visibility.Collapsed;
        PageTitle.Text = new[] { "组件总览", "基础交互控件", "任务与数据", "外观设置" }[index];
        PageDescription.Text = new[] { "Windows Fluent 视觉规范、组件状态与交互行为。", "体验按钮、表单、开关、滑块和即时反馈。", "展示可交互的任务状态管理。", "调整全局颜色、主题和外观偏好。" }[index];
    }

    private void Navigate_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button b && int.TryParse(b.Tag?.ToString(), out int index)) Navigate(index);
    }
    private void Explore_Click(object sender, RoutedEventArgs e) => Navigate(1);
    private void Notify_Click(object sender, RoutedEventArgs e) => MessageBox.Show(this, "这是一条可交互的示例通知。", "WpfFluentKit", MessageBoxButton.OK, MessageBoxImage.Information);
    private void Action_Click(object sender, RoutedEventArgs e) => StatusText.Text = "●  按钮点击成功";
    private void Theme_Click(object sender, RoutedEventArgs e)
    {
        _dark = !_dark;
        DarkModeToggle.IsChecked = _dark;
        ApplyTheme();
    }
    private void DarkMode_Changed(object sender, RoutedEventArgs e)
    {
        if (DarkModeToggle is null) return;
        _dark = DarkModeToggle.IsChecked == true;
        ApplyTheme();
    }
    private void ApplyTheme()
    {
        if (Application.Current is null) return;
        ThemeManager.Apply(_dark, _accent);
        StatusText.Text = _dark ? "●  已切换深色主题" : "●  已切换浅色主题";
    }
    private void Accent_Click(object sender, RoutedEventArgs e) { Navigate(3); }
    private void AccentBlue_Click(object sender, RoutedEventArgs e) => SetAccent("#6677EE");
    private void AccentGreen_Click(object sender, RoutedEventArgs e) => SetAccent("#12A89D");
    private void AccentOrange_Click(object sender, RoutedEventArgs e) => SetAccent("#DD714F");
    private void Reset_Click(object sender, RoutedEventArgs e)
    {
        _dark = true;
        _accent = (Color)ColorConverter.ConvertFromString("#6677EE");
        DarkModeToggle.IsChecked = true;
        ApplyTheme();
    }
    private void SetAccent(string hex)
    {
        _accent = (Color)ColorConverter.ConvertFromString(hex);
        ApplyTheme();
    }
    private void DemoSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (DemoProgress is not null) DemoProgress.Value = e.NewValue;
    }
    private void AddTask_Click(object sender, RoutedEventArgs e)
    {
        var name = string.IsNullOrWhiteSpace(TaskSearch.Text) ? $"演示任务 {_tasks.Count + 1}" : TaskSearch.Text.Trim();
        var task = new DemoTask(name, "待运行");
        _tasks.Add(task);
        TaskList.Items.Refresh();
        TaskList.SelectedItem = task;
        TaskStatus.Text = $"已添加：{name}";
    }
    private void TaskSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (TaskList is null) return;
        var term = TaskSearch.Text.Trim();
        var view = System.Windows.Data.CollectionViewSource.GetDefaultView(_tasks);
        view.Filter = item => item is DemoTask t && (term.Length == 0 || t.Name.Contains(term, StringComparison.OrdinalIgnoreCase));
        view.Refresh();
    }
    private void TaskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (TaskList.SelectedItem is DemoTask t) TaskStatus.Text = $"{t.Name} · {t.Status}";
    }
    private void StartTask_Click(object sender, RoutedEventArgs e) => UpdateTask("进行中");
    private void FinishTask_Click(object sender, RoutedEventArgs e) => UpdateTask("已完成");
    private void UpdateTask(string status)
    {
        if (TaskList.SelectedItem is not DemoTask t) { TaskStatus.Text = "请先选择一项任务"; return; }
        t.Status = status;
        TaskList.Items.Refresh();
        TaskStatus.Text = $"{t.Name} · {status}（演示状态，不执行外部任务）";
    }
    private void RemoveTask_Click(object sender, RoutedEventArgs e)
    {
        if (TaskList.SelectedItem is DemoTask t) { _tasks.Remove(t); TaskStatus.Text = $"已移除：{t.Name}"; }
    }

    private sealed class DemoTask(string name, string status)
    {
        public string Name { get; } = name;
        public string Status { get; set; } = status;
        public override string ToString() => $"{Name} · {Status}";
    }
}