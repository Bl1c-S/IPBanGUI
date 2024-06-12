using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility.Services;
using Logic_IPBanUtility.Setting;
using Logic_IPBanUtility.Setting.Builders;
using System.Linq;
using System.Windows.Input;
using WPF_IPBanUtility.Base;

namespace WPF_IPBanUtility;

public class ClearLogsViewModel : SettingsComponentViewModelBase
{
     private readonly FileManager _fileManager = new();
     private readonly LogFilePathExtractor _logFilePathExtractor;
     public ICommand IClearLogsCommand { get; set; }
     public ClearLogsViewModel(Settings settings) : base(Properties.PageNames.ClearLogsTitle)
     {
          IClearLogsCommand = new RelayCommand(DeleteLogFiles);
          _logFilePathExtractor = new(settings.IPBan.Folder);
     }
     private void DeleteLogFiles()
     {
          var paths = _logFilePathExtractor.GetDaysWithLogFilePath().Values.ToArray();
          _fileManager.DeleteFiles(paths);
     }
     public override void Save() { }
}