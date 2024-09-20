using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

using System.Windows.Input;

using WPF_IPBanUtility.Properties;

namespace WPF_IPBanUtility;

internal class ManualViewModel : PageViewModelBase
{
     public string WelcomeParagraph { get => Manual.WelcomeParagraph; }

     public string KeyTitle { get => PageNames.KeyList; }
     public string KeyParagraph1 { get => Manual.KeyParagraph1; }
     public string KeyParagraph2 { get => Manual.KeyParagraph2; }

     public string EventTitle { get => PageNames.Events; }
     public string EventParagraph1 { get => Manual.EventParagraph1; }
     public string EventParagraph2 { get => Manual.EventParagraph2; }

     public string SupportTitle { get => PageNames.Support; }
     public string SupportDescription { get => Messages.SupportDescription; }
     public string SupportMail { get => "https://mail.google.com/mail/u/0/?tab=rm&ogbl#search/kubarych.torgsoft%40gmail.com?compose=new"; }
     public string GitHubDiscussions { get => "https://github.com/Bl1c-S/IPBanGUI/discussions"; }

     public string UpdateTitle { get => PageNames.UpdateTitle; }
     public string UpdateParagraph1 { get => Manual.UpdateParagraph1; }
     public string UpdateParagraph2 { get => Manual.UpdateParagraph2; }
     public string Updates { get => "https://github.com/Bl1c-S/IPBanGUI/releases"; }

     public ManualViewModel() : base(PageNames.Manual)
     {
          IOpenLinkCommand = new RelayCommand(OpenLink);
          CreatePageButtons();
     }
     private ICommand IOpenLinkCommand { get; }
     private void OpenLink()
     {
          Process.Start(new ProcessStartInfo(Updates) { UseShellExecute = true });
     }
     protected override void CreatePageButtons()
     {
          PageButtons.Add(CreateButtonWithTitle(
               IOpenLinkCommand, Wpf.Ui.Common.SymbolRegular.ApprovalsApp28, UpdateTitle));
     }
}
