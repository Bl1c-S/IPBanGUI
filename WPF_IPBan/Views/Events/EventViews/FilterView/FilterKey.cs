using CommunityToolkit.Mvvm.ComponentModel;
using Logic_IPBanUtility.Logic.LogFile;

namespace WPF_IPBanUtility;

public partial class FilterKey : ObservableObject
{
     [ObservableProperty]
     private bool _isEnable;

     [ObservableProperty]
     private int _count; 

     public string Name { get; }
     public LogEventType Type { get; }
     public FilterKey(string name, bool isEnable, LogEventType type)
     {
          Name = name;
          Type = type;
          _isEnable = isEnable;
     }
}
