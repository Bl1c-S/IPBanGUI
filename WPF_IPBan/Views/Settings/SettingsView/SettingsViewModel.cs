using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility.Setting;
using System.Collections.Generic;
using System.Windows.Input;
using Wpf.Ui.Controls;
namespace WPF_IPBanUtility;

internal class SettingsViewModel : PageViewModelBase
{
     private readonly SettingsVMsBuilder _settingsVMsBuilder;
     public List<ISettingsVMComponent> VMs => _vMs;
     private List<ISettingsVMComponent> _vMs;

     public SettingsViewModel(SettingsVMsBuilder settingsVMsBuilder) : base(Properties.PageNames.Settings)
     {
          _settingsVMsBuilder = settingsVMsBuilder;
          _vMs = settingsVMsBuilder.Build();
          ISaveChangedCommand = new RelayCommand(SaveChanged);
          ISetDefaultSettingsCommand = new RelayCommand(SetDefaultSettings);
          CreatePageButtons();
     }

     #region SaveChanged
     public ICommand ISaveChangedCommand { get; }
     public void SaveChanged()
     {
          foreach (var vm in VMs)
               vm.Save();
     }
     #endregion

     public ICommand ISetDefaultSettingsCommand { get; }
     public void SetDefaultSettings()
     {
          var sb = new SettingsBuilder();
          sb.LoadSettings();
          var iPBan = sb.Settings!.IPBan; // Якщо ви увійшли в програму, значить ви 100% встановили коректний IPBan.
          sb.CreateDefaultSettings(iPBan);
          _vMs = _settingsVMsBuilder.Build();
          OnPropertyChanged(nameof(VMs));
     }

     public override void CreatePageButtons()
     {
          PageButtons.Add(new Button { Content = Properties.ButtonNames.Save, Command = ISaveChangedCommand , 
               Icon = Wpf.Ui.Common.SymbolRegular.SaveMultiple24 });
          PageButtons.Add(new Button { Content = Properties.ButtonNames.Default, Command = ISetDefaultSettingsCommand, 
               Icon = Wpf.Ui.Common.SymbolRegular.LauncherSettings24, Margin = new(4,0,0,0)});
     }
}