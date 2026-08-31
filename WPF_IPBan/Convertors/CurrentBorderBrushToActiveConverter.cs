using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using WPF_IPBanUtility.Properties;

namespace WPF_IPBanUtility;

public class CurrentBorderBrushToActiveConverter : IMultiValueConverter
{
     public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
     {
          var currentPageName = values.Length > 0 ? values[0] as string : null;
          var title = values.Length > 1 ? values[1] as string : null;
          var color = currentPageName is not null && currentPageName == title
               ? Collors.Active
               : Collors.InActive;

          return new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
     }

     public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
     {
          throw new NotSupportedException();
     }
}
