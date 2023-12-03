namespace WPF_IPBanUtility;

internal interface ISettingsViewComponent
{
     public bool IsChanged { get; set; }
     public void Save();
}