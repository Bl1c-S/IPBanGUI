namespace WPF_IPBanUtility.Base;

public class SettingsComponentViewModelBase : ViewModelBase, ISettingsVMComponent
{
     public SettingsComponentViewModelBase(string title)
     {
          Title = title;
     }

     public string Title { get; }
     public virtual void Save() { }
}
