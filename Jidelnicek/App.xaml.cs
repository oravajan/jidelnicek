using System.Windows;
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
}