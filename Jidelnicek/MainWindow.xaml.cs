using System.Windows;
using Jidelnicek.ViewModels;

namespace Jidelnicek
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void CloseBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}