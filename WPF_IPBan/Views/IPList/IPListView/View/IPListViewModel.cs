using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Windows.Input;
using WPF_IPBanUtility.Views.IPList.IPListView.View;
using Button = Wpf.Ui.Controls.Button;

namespace WPF_IPBanUtility;

public class IPListViewModel : PageViewModelBase
{    
     public List<IPListViewModelBase> VMs { get; }
     public IPListViewModel(IPListVMsBuilder vMsBuilder) : base(Properties.PageNames.IP)
     {
          VMs = vMsBuilder.Build();
          IUpdateAllCommand = new RelayCommand(UpdateAll);
          CreatePageButtons();
     }

     #region Update
     public ICommand IUpdateAllCommand { get; }
     private void UpdateAll()
     {
     }
     #endregion

     protected override void CreatePageButtons()
     {
          PageButtons.Add(new Button
          {
               Content = Properties.ButtonNames.Update,
               Command = IUpdateAllCommand,
               Icon = Wpf.Ui.Common.SymbolRegular.ArrowSync24
          });
     }
}