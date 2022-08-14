using System.Windows;

namespace Jidelnicek;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void CloseBtn_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}