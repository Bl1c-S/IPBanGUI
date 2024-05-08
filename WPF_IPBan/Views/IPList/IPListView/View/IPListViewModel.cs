using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Windows.Input;
using WPF_IPBanUtility.Views.IPList.IPListView.View;
using Button = Wpf.Ui.Controls.Button;

namespace WPF_IPBanUtility;

public class IPListViewModel : PageViewModelBase
{    
     public List<IPListViewModelBase> VMs { get; }
     public IPListViewModel(IPListProperties properties, IPListVMsBuilder vmsBuilder) : base(Properties.PageNames.IP)
     {
          VMs = vmsBuilder.Build(properties);
          IUpdateAllCommand = new RelayCommand(UpdateAll);
          CreatePageButtons();
     }

     #region Update
     public ICommand IUpdateAllCommand { get; }
     private void UpdateAll() => VMs.ForEach(x => x.Update());
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