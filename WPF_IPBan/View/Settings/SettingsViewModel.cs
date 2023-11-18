using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Input;
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
          _dirrectoryPath = _settings.DirrectoryPath;
          ISaveChangedCommand = new RelayCommand(TrySaveChanged);
          IOpenFolderCommand = new RelayCommand(SelectFolder);
     }

     private void SelectFolder()
     {
          using (var dialog = new FolderBrowserDialog())
          {
               DialogResult result = dialog.ShowDialog();

               if (result == DialogResult.OK)
               {
                    string selectedPath = dialog.SelectedPath;
                    DirrectoryPath = selectedPath;
               }
          }
     }

     private void TrySaveChanged()
     {
          try
          {
               _settings.SaveChanged(_dirrectoryPath);
          }
          catch (Exception e)
          {
               InfoMessageBox.OpenMassangeBox("Помилка збереження", e.Message);
          }
     }
}