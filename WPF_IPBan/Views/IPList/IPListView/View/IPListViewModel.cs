using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility.Logic.ConfigFile;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WPF_IPBanUtility.Views.IPList;
using Button = Wpf.Ui.Controls.Button;

namespace WPF_IPBanUtility;

public class IPListViewModel : PageViewModelBase
{
     public IPListProperties MyProperties { get; set; }
     public IPInputViewModel IPInputVM { get; }
     public List<IPListViewModelBase> VMs { get; }

     private readonly IPListVMsBuilder _vmsBuilder;

     public IPListViewModel(IPListProperties properties, IPListVMsBuilder vmsBuilder, KeyValueManager keyManager) : base(Properties.PageNames.IP)
     {
          _vmsBuilder = vmsBuilder;
          MyProperties = properties;
          var vmsResult = _vmsBuilder.Build(MyProperties);
          VMs = vmsResult.IPListVMs;
          IPInputVM = vmsResult.IPInputVM;

          IUpdateAllCommand = new RelayCommand(UpdateAll);
          IAddIPCommand = new RelayCommand(ChangeIPInputVisibility);
          CreatePageButtons();
     }

     #region Add
     public ICommand IAddIPCommand { get; }
     private void ChangeIPInputVisibility()
     {
          if (MyProperties.IPInputVisibility != Visibility.Visible)
               MyProperties.IPInputVisibility = Visibility.Visible;
          else
               MyProperties.IPInputVisibility = Visibility.Collapsed;
          OnPropertyChanged(nameof(MyProperties));
     }
     #endregion

     #region Update
     public ICommand IUpdateAllCommand { get; }
     private void UpdateAll() => VMs.ForEach(x => x.Update());
     #endregion

     protected override void CreatePageButtons()
     {
          PageButtons.Add(new Button
          {
               Content = Properties.ButtonNames.Add,
               Command = IAddIPCommand,
               Icon = Wpf.Ui.Common.SymbolRegular.Add24
          });
          PageButtons.Add(new Button
          {
               Content = Properties.ButtonNames.Update,
               Command = IUpdateAllCommand,
               Icon = Wpf.Ui.Common.SymbolRegular.ArrowSync24,
               Margin = new(4, 0, 0, 0)
          });
     }

     public override void Dispose()
     {
          IPInputVM.Dispose();
          VMs.ForEach(vm => vm.Dispose());
          VMs.Clear();
          _vmsBuilder.Dispose();
          base.Dispose();
     }
}