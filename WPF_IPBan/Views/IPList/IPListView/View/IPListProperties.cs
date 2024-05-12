using System.Windows;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility
{
     public class IPListProperties
     {
          public Visibility IPInputVisibility { get; set; } = Visibility.Collapsed;
          public IPListViewProperties BlockList = new(true);
          public IPListViewProperties WhiteList = new(false);
          public IPListViewProperties BlackList = new(false);
     }
}