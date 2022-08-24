using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Jidelnicek.Views;

public class DateConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        DateTime date = (DateTime)value;
        return date == DateTime.MinValue ? "Nevařeno" : value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotSupportedException();
    }
}

public class CountConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int cnt = (int)value;
        return cnt.ToString() + "x";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotSupportedException();
    }
}

public partial class ListFoodView : UserControl
{
    public ListFoodView()
    {
        InitializeComponent();
    }
}