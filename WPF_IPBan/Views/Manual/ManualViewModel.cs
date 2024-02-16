using WPF_IPBanUtility.Properties;

namespace WPF_IPBanUtility;

internal class ManualViewModel : PageViewModelBase
{
     public string WelcomeParagraph { get => Manual.WelcomeParagraph; }
     public string Paragraph1 { get => Manual.Paragraph1; }
     public SupportViewModel SupportVM { get; set; }
     public ManualViewModel() : base(PageNames.Manual)
     {
          SupportVM = new SupportViewModel();
     }
}
