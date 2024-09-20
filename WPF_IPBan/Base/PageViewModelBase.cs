using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;
using Wpf.Ui.Controls;
using WPF_IPBanUtility.Properties;

namespace WPF_IPBanUtility;

public class PageViewModelBase : ViewModelBase
{
     private const int INFO = 0;
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
     public virtual void ApplyChanges(ApplyOptions[]? options = null) { }

     protected virtual void CreatePageButtons()
     {
          var activeColor = (Color)ColorConverter.ConvertFromString(Collors.Active);
          PageButtons.Add(new Button()
          {
               Icon = Wpf.Ui.Common.SymbolRegular.ErrorCircle24,
               ToolTip = "",
               BorderBrush = new SolidColorBrush(activeColor),
               Visibility = Visibility.Collapsed,
               Margin = new(0, 0, 8, 0)
          });
     }
     protected void ChangeInfoVisibility(bool isVisible)
     {
          PageButtons[INFO].Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
          OnPropertyChanged(nameof(PageButtons));
     }
     protected void ChangeInfoMessage(string message)
     {
          PageButtons[INFO].ToolTip = message;
          OnPropertyChanged(nameof(PageButtons));
     }
     public override void Dispose()
     {
          base.Dispose();
     }
}

public enum ApplyOptions
{
     None,
     Await
}