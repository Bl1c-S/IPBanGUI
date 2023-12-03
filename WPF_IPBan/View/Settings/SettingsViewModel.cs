using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Forms;
using System.Windows.Input;
using Logic_IPBanUtility.Setting;
using Logic_IPBanUtility;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace WPF_IPBanUtility;

internal class SettingsViewModel : ViewModelBase
{
     public ICommand ISaveChangedCommand { get; }

     public SettingsViewModel()
     {
          ISaveChangedCommand = new RelayCommand(SaveChanged);
     }
     public void SaveChanged()
     {

     }
}