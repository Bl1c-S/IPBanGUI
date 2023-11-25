using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Forms;
using System.Windows.Input;
using Logic_IPBanUtility.Setting;
using Logic_IPBanUtility;

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
               OnPropertyChanged(nameof(DirrectoryPath));
          }
     }
     public ICommand ISaveChangedCommand { get; }
     public ICommand IOpenFolderCommand { get; }

     public SettingsViewModel(Settings settings)
     {
          _settings = settings;
          _dirrectoryPath = GetIPBanFolder();
          ISaveChangedCommand = new RelayCommand(SaveSettings);
          IOpenFolderCommand = new RelayCommand(SelectFolder);
     }

     private string GetIPBanFolder()
     {
          if (_settings.IPBan is null)
               return string.Empty;
          else return _settings.IPBan.Folder;
     }

     private void SelectFolder()
     {
          using (var dialog = new FolderBrowserDialog())
          {
               DialogResult result = dialog.ShowDialog();
               if (result == DialogResult.OK)
                    DirrectoryPath = dialog.SelectedPath;
          }
     }

     private void SaveSettings()
     {
          try
          {
               if (GetIPBanFolder() != _dirrectoryPath)
                    _settings.IPBan = IPBan.Create(_dirrectoryPath);

               _settings.Save();
          }
          catch (Exception e)
          {
               DialogMessageBox.InfoBox("Помилка збереження", e.Message);
          }
     }
}