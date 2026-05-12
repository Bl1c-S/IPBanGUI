using Logic_IPBanUtility.Interfaces.Services;
using System.Collections.ObjectModel;
using WPF_IPBanUtility.Base;

namespace WPF_IPBanUtility;

public class WinServicesViewModel : SettingsComponentViewModelBase
{
     public ObservableCollection<ServiceViewModel> VMs { get; private set; }
     public IWinServicesController Controller { get; }
     public WinServicesViewModel(IWinServicesController controller) : base(Properties.PageNames.Services)
     {
          Controller = controller;
          VMs = new() { new(controller.IPBan) };
     }
     public override void Save() { }
}