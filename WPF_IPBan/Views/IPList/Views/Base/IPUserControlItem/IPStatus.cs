using Wpf.Ui.Common;

namespace WPF_IPBanUtility.Views.IPList
{
     public class IPStatus
     {
          public IPStatus(SymbolRegular statusIcon, string statusTitle, string statusMessage)
          {
               Icon = statusIcon;
               Title = statusTitle;
               Message = statusMessage;
          }

          public SymbolRegular Icon { get; }
          public string Title { get; }
          public string Message { get; }
     }
}
