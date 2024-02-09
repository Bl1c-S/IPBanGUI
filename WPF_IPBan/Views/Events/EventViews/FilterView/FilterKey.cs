using CommunityToolkit.Mvvm.ComponentModel;

namespace Logic_IPBanUtility.Logic.LogFile.Models;

public class FilterKey : ObservableObject
{
     public delegate void FilterChangedDelegate(FilterKey filterKey);
     private readonly FilterChangedDelegate _filterChanged;
     public LogEventType Type { get; set; }
     public string Name { get; private set; }
     public bool IsEnable
     {
          get => _isEnable;
          set
          {
               _isEnable = value;
               _filterChanged(this);
               OnPropertyChanged(nameof(IsEnable));
          }
     }
     private bool _isEnable;
     public FilterKey(string name, LogEventType type, FilterChangedDelegate filterChanged)
     {
          Name = name;
          Type = type;
          _isEnable = true;
          _filterChanged = filterChanged;
     }
}
