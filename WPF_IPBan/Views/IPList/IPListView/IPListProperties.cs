using System.Windows;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility
{
     public class IPListProperties
     {
          public Visibility IPInputVisibility { get; set; } = Visibility.Collapsed;
          public IPListViewProperties BlockList { get; set; } = new(true);
          public IPListViewProperties WhiteList { get; set; } = new(false);
          public IPListViewProperties BlackList { get; set; } = new(false);
     }
}