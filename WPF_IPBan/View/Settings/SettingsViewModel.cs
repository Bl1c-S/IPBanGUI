using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility;
using System.Windows.Input;

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
          ISaveChangedCommand = new RelayCommand(() => _settings.SaveChanged(_dirrectoryPath));
     }
     public ICommand ISaveChangedCommand { get; }


}