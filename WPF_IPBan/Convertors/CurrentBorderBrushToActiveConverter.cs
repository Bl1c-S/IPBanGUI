using System;
using System.Globalization;
using System.Windows.Data;
using WPF_IPBanUtility.Properties;

namespace WPF_IPBanUtility;

public class CurrentBorderBrushToActiveConverter : IValueConverter
{
     public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
     {
          if (value is string currentPageName && parameter is string selectedPageName)
          {
               return currentPageName != selectedPageName ? Collors.InActive : Collors.Active;
          }
          return string.Empty;
     }

     public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
     {
          throw new NotImplementedException();
     }
}
