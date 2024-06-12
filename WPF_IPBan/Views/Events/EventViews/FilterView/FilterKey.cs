using CommunityToolkit.Mvvm.ComponentModel;
using Logic_IPBanUtility.Logic.LogFile;

namespace WPF_IPBanUtility;

public class FilterKey : ObservableObject
{
     public delegate void FilterChangedDelegate(bool state, LogEventType type);
     private readonly FilterChangedDelegate _filterChanged;
     public LogEventType Type { get; set; }
     public string Name { get; private set; }
     public bool IsEnable
     {
          get => _isEnable;
          set
          {
               _isEnable = value;
               _filterChanged(value, Type);
               OnPropertyChanged(nameof(IsEnable));
          }
     }
     private bool _isEnable;
     public FilterKey(string name, bool isEnable, LogEventType type, FilterChangedDelegate filterChanged)
     {
          Name = name;
          Type = type;
          _isEnable = isEnable;
          _filterChanged = filterChanged;
     }
}
