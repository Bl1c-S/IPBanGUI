using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Windows.Input;
namespace WPF_IPBanUtility;

internal class SettingsViewModel : ViewModelBase
{
     public ICommand ISaveChangedCommand { get; }
     public List<ISettingsVMComponent> VMs { get; }

     public SettingsViewModel(List<ISettingsVMComponent> vMs)
     {
          VMs = vMs;
          ISaveChangedCommand = new RelayCommand(SaveChanged);
     }
     public void SaveChanged()
     {
          foreach (var vm in VMs)
               vm.Save();
     }
}