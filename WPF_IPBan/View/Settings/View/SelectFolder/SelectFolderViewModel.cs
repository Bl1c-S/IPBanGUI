using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility;
using System;
using System.Windows.Forms;
using System.Windows.Input;
using Logic_IPBanUtility.Setting;
using System.Runtime;

namespace WPF_IPBanUtility;

internal class SelectFolderViewModel : ViewModelBase
{
     private Settings _settings;

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
     public ICommand IOpenFolderCommand { get; }
     public bool IsChanged => _dirrectoryPath != _settings.IPBan.Folder;

     public SelectFolderViewModel(Settings settings)
     {
          _settings = settings;
          _dirrectoryPath = GetIPBanFolder();
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

     public void Save()
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
