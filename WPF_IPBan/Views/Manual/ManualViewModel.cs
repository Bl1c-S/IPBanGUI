using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

using System.Windows.Input;

using WPF_IPBanUtility.Properties;

namespace WPF_IPBanUtility;

internal class ManualViewModel : PageViewModelBase
{
     public string DocsTitle { get => PageNames.Docs; }
     public string UpdateTitle { get => PageNames.UpdateTitle; }
     public string SupportTitle { get => PageNames.Support; }
     public string SupportDescription { get => Messages.SupportDescription; }
     public string SupportMail { get => "https://mail.google.com/mail/u/0/?tab=rm&ogbl#search/kubarych.torgsoft%40gmail.com?compose=new"; }
     public string GitHubDiscussions { get => "https://github.com/Bl1c-S/IPBanGUI/discussions"; }
     public string Updates { get => "https://ipban-gui-docs.torgsoft.ua/update/last"; }
     public string DocsSite { get => "https://ipban-gui-docs.torgsoft.ua/"; }

     public ManualViewModel() : base(PageNames.Manual)
     {
          IOpenUpdateLinkCommand = new RelayCommand(OpenUpdates);
          IOpenDocsLinkCommand = new RelayCommand(OpenDocs);
          CreatePageButtons();
     }
     private ICommand IOpenUpdateLinkCommand { get; }
     private ICommand IOpenDocsLinkCommand { get; }
     private void OpenUpdates()
     {
          Process.Start(new ProcessStartInfo(Updates) { UseShellExecute = true });
     }
     private void OpenDocs()
     {
          Process.Start(new ProcessStartInfo(DocsSite) { UseShellExecute = true });
     }
     protected override void CreatePageButtons()
     {
          PageButtons.Add(CreateButtonWithTitle(
               IOpenDocsLinkCommand, Wpf.Ui.Common.SymbolRegular.BookQuestionMark24, DocsTitle, "Посилання на сайт документацію"));
          PageButtons.Add(CreateButtonWithTitle(
               IOpenUpdateLinkCommand, Wpf.Ui.Common.SymbolRegular.ApprovalsApp28, UpdateTitle, "Посилання на сторінку з оновленнями в документації програми", new(4, 0, 0, 0)));
     }
}
