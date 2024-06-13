using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace WPF_IPBanUtility;

public class PageViewModelBase : ViewModelBase
{
     public string PageName { get; set; }

     public bool PageHaveChanges;
     protected virtual void PageChanged() => PageHaveChanges = true;
     public ObservableCollection<Button> PageButtons { get; set; }
     public PageViewModelBase(string pageName)
     {
          PageName = pageName;
          PageHaveChanges = false;
          PageButtons = new ObservableCollection<Button>();
     }
     public virtual bool ApplyChanges(ApplyOptions[]? options = null) => true;

     protected virtual void CreatePageButtons()
     {
     }
}

public enum ApplyOptions
{
     None,
     Await
}