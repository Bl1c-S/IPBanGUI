using System.Windows.Forms;
using WPF_IPBanUtility.Base;

namespace WPF_IPBanUtility;

internal class SupportViewModel : SettingsComponentViewModelBase
{
     public string SupportDescription { get => Properties.Messages.SupportDescription; }
     public string SupportMail { get => "https://mail.google.com/mail/u/0/?tab=rm&ogbl#search/kubarych.torgsoft%40gmail.com?compose=new"; }
     public string GitHubDiscussions { get => "https://github.com/Bl1c-S/IPBanGUI/discussions"; }
     public SupportViewModel()
     {
          Title = Properties.PageNames.Support;
     }
     public override void Save()
     {
     }
}
