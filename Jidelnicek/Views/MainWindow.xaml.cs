using System.Windows;

namespace Jidelnicek.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
    }

    private void CloseBtn_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
    
    private void MaximizeBtn_OnClick(object sender, RoutedEventArgs e)
    {
        if (WindowState == WindowState.Normal)
            WindowState = WindowState.Maximized;
        else
            WindowState = WindowState.Normal;
    }
    
    private void MinimizeBtn_OnClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
}