using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility;
using System;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace WPF_IPBanUtility;

internal class SettingsViewModel : ViewModelBase
{
     private Settings _settings { get; set; }

     private string _dirrectoryPath;
     public string DirrectoryPath
     {
          get => _dirrectoryPath;
          set
          {
               _dirrectoryPath = value;
               OnPropertyChanging(nameof(DirrectoryPath));
          }
     }

     public SettingsViewModel(Settings settings)
     {
          _settings = settings;
          _dirrectoryPath = _settings.DirrectoryPath;
          ISaveChangedCommand = new RelayCommand(TrySaveChanged);
     }
     public ICommand ISaveChangedCommand { get; }

     private void TrySaveChanged()
     {
          try
          {
               _settings.SaveChanged(_dirrectoryPath);
          }
          catch (Exception e)
          {
               InfoMessageBox messageBox = new(e.Message, "Error", "Try again", "Close");
               messageBox.OpenMassangeBox(TrySaveChanged);
          }
     }
}