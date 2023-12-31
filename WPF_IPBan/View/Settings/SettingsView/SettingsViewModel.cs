﻿using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility.Setting;
using System.Collections.Generic;
using System.Windows.Input;
namespace WPF_IPBanUtility;

internal class SettingsViewModel : ViewModelBase
{
     private readonly SettingsVMsBuilder _settingsVMsBuilder;
     public List<ISettingsVMComponent> VMs  => _vMs; 
     private List<ISettingsVMComponent> _vMs;

     public SettingsViewModel(SettingsVMsBuilder settingsVMsBuilder)
     {
          _settingsVMsBuilder = settingsVMsBuilder;
          _vMs = settingsVMsBuilder.Build();
          ISaveChangedCommand = new RelayCommand(SaveChanged);
          ISetDefaultSettingsCommand = new RelayCommand(SetDefaultSettings);
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
}