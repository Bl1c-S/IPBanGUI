using System.Windows;
using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility
{
     public class IPListProperties
     {
          public Visibility IPInputVisibility { get; set; } = Visibility.Collapsed;
          public readonly IPListViewProperties BlockList = new(PageNames.BlockList, true);
          public readonly IPListViewProperties WhiteList = new(PageNames.WhiteList, false);
          public readonly IPListViewProperties BlackList = new(PageNames.BlackList, false);
     }
}