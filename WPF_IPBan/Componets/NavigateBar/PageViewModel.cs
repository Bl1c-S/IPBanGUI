namespace WPF_IPBanUtility;

internal class PageViewModel : ViewModelBase
{
     public PageViewModel(string pageName)
     {
          PageName = pageName;
     }

     public string PageName { get; set; }
}
