using System;
using System.Globalization;
using System.Windows.Data;

namespace WPF_IPBanUtility;

public class CurrentViewToActiveConverter : IValueConverter
{
     public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
     {
          if(value is ViewModelBase currentView && parameter is string selectedView)
          {
               var currentViewString =  currentView.GetType().Name.ToLower();
               return currentViewString != selectedView.ToLower();
          }
          return true;
     }

     public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
     {
          throw new NotImplementedException();
     }
}
