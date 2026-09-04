using System;
using System.Globalization;
using System.Windows.Data;

namespace WPF_IPBanUtility;

public class CurrentPageNameToActiveConverter : IMultiValueConverter
{
     public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
     {
          var currentPageName = values.Length > 0 ? values[0] as string : null;
          var title = values.Length > 1 ? values[1] as string : null;

          return currentPageName is not null && currentPageName == title
               ? title ?? string.Empty
               : string.Empty;
     }

     public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
     {
          throw new NotSupportedException();
     }
}
