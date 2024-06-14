namespace WPF_IPBanUtility.Base;

public class SettingsComponentViewModelBase : ViewModelBase, ISettingsVMComponent
{
     public SettingsComponentViewModelBase(string title)
     {
          Title = title;
     }
     protected bool IsChanged = false;
     protected virtual void Changed() { }
     public string Title { get; }
     public virtual void Save() { }
}
