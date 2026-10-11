using System.Windows;
using System.Windows.Media;

namespace WpfFluentKit;

public static class ThemeManager
{
    public static void Apply(bool dark, Color accent)
    {
        SetBrush("KitBackgroundBrush", dark ? "#101218" : "#F5F6F9");
        SetBrush("KitSurfaceBrush", dark ? "#191C24" : "#FFFFFFFF");
        SetBrush("KitSurfaceHoverBrush", dark ? "#252937" : "#FFEAEFFA");
        SetBrush("KitTextBrush", dark ? "#F5F7FC" : "#FF1D2532");
        SetBrush("KitSecondaryTextBrush", dark ? "#9EA7B9" : "#FF687386");
        SetBrush("KitBorderBrush", dark ? "#343949" : "#FFE1E5ED");
        Application.Current.Resources["KitAccentColor"] = accent;
        Application.Current.Resources["KitAccentBrush"] = new SolidColorBrush(accent);
        Wpf.Ui.Appearance.ApplicationThemeManager.Apply(
            dark ? Wpf.Ui.Appearance.ApplicationTheme.Dark : Wpf.Ui.Appearance.ApplicationTheme.Light);
    }

    private static void SetBrush(string key, string hex) =>
        Application.Current.Resources[key] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hex));
}