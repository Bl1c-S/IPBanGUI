using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace WPF_IPBanUtility;

internal class PageViewModelBase : ViewModelBase
{
     public PageViewModelBase(string pageName)
     {
          PageName = pageName;
          PageButtons = new ObservableCollection<Button>();
     }

     public string PageName { get; set; }
     public ObservableCollection<Button> PageButtons { get; set; }

     public virtual void CreatePageButtons()
     {
     }
}
