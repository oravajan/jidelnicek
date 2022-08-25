using System.Windows;
using System.Windows.Media.Effects;
using Jidelnicek.ViewModels;
using Jidelnicek.Views;

namespace Jidelnicek;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow = new MainWindow
        {
            DataContext = new MainViewModel()
        };
        MainWindow.Show();

        base.OnStartup(e);
    }
    
    public static void SetBlurEffect(bool set, Window? ignoreWindow)
    {
        foreach (Window win in Current.Windows)
        {
            if (win == ignoreWindow) continue;
            win.Effect = set ? new BlurEffect() : null;
        }
    }
}