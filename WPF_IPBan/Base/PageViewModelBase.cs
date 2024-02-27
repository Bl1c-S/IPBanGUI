using System.Collections.ObjectModel;
using System.Windows;
using Wpf.Ui.Controls;

namespace WPF_IPBanUtility;

public class PageViewModelBase : ViewModelBase
{
     public PageViewModelBase(string pageName)
     {
          PageName = pageName;
          PageButtons = new ObservableCollection<Button>();
     }

     public string PageName { get; set; }
     public ObservableCollection<Button> PageButtons { get; set; }

     protected virtual void CreatePageButtons()
     {
     }
}
