using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
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
     private readonly WinServicesController _servicesController;

     public IPListViewModel(IPListProperties properties, IPListVMsBuilder vmsBuilder, WinServicesController servicesController) : base(Properties.PageNames.IP)
     {
          _vmsBuilder = vmsBuilder;
          _servicesController = servicesController;
          MyProperties = properties;
          var vmsResult = _vmsBuilder.Build(MyProperties);
          VMs = vmsResult.IPListVMs;
          IPInputVM = vmsResult.IPInputVM;

          foreach (var vm in VMs)
               vm.VMChanged += PageChanged;

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

     public override bool ApplyChanges()
     {
          if (PageHaveChanges)
               _servicesController.IPBan.Restart().Wait();
          PageHaveChanges = false; //Для того що двічі не перезавантажувалась служба при закритті вікна.
          return true;
     }
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
          ApplyChanges();
          foreach (var vm in VMs)
          {
               vm.VMChanged -= PageChanged;
               vm.Dispose();
          }
          VMs.Clear();

          IPInputVM.Dispose();
          _vmsBuilder.Dispose();
          base.Dispose();
     }
}