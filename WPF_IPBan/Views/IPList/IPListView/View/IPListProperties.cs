using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility
{
     public class IPListProperties
     {
          public IPListViewProperties BlockList = new(true);
          public IPListViewProperties WhiteList = new(false);
          public IPListViewProperties BlackList = new(false);
     }
}