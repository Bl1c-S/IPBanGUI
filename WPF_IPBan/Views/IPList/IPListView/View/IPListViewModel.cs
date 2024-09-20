using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility;

public class IPListViewModel : PageViewModelBase
{
     public IPListProperties MyProperties { get; set; }
     public IPInputViewModel IPInputVM { get; }
     public List<IPListViewModelBase> VMs { get; }

     private readonly IPListVMsBuilder _vmsBuilder;
     private readonly WinServicesController _servicesController;

     public IPListViewModel(IPListProperties properties, IPListVMsBuilder vmsBuilder, WinServicesController servicesController) : base(PageNames.IP)
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

     #region Changed
     protected override void PageChanged()
     {
          base.PageChanged();
          ChangeInfoVisibility(PageHaveChanges);
     }
     public override void ApplyChanges(ApplyOptions[]? options = null)
     {
          if (PageHaveChanges)
          {
               if (options != null && options.Contains(ApplyOptions.Await))
                    _servicesController.IPBan.Restart().Wait();
               else _servicesController.IPBan.Restart();
          }
          PageHaveChanges = false;
     }
     #endregion

     protected override void CreatePageButtons()
     {
          base.CreatePageButtons();
          ChangeInfoMessage(ToolTips.ReloadIPBanService);

          PageButtons.Add(CreateButtonWithTitle(
               IAddIPCommand, Wpf.Ui.Common.SymbolRegular.Add24,
               ButtonNames.Add, ToolTips.AddIpView));
          PageButtons.Add(CreateButtonWithTitle(
               IUpdateAllCommand, Wpf.Ui.Common.SymbolRegular.ArrowSync24,
               ButtonNames.Update, ToolTips.UpdateIPLists, new(4, 0, 0, 0)));
     }

     public override void Dispose()
     {
          ApplyChanges(new[] { ApplyOptions.Await });
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