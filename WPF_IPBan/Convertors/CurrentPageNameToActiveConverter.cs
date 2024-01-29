using System;
using System.Globalization;
using System.Windows.Data;

namespace WPF_IPBanUtility;

public class CurrentPageNameToActiveConverter : IValueConverter
{
     public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
     {
          if (value is string currentPageName && parameter is string selectedPageName)
          {
               return currentPageName != selectedPageName ? string.Empty : selectedPageName;
          }
          return string.Empty;
     }

     public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
     {
          throw new NotImplementedException();
     }
}
