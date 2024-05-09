using CommunityToolkit.Mvvm.ComponentModel;

namespace WPF_IPBanUtility.Views.IPList;

public class IPListViewProperties : ObservableObject
{
     public bool IsExpanded { get; set; }

     public IPListViewProperties(bool isExpanded)
     {
          IsExpanded = isExpanded;
     }
}
