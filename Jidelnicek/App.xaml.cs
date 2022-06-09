using System.Windows;
using Jidelnicek.ViewModels;

namespace Jidelnicek
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(),
                MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight
            };
            MainWindow.Show();
            
            base.OnStartup(e);
        }
    }
}