namespace WPF_IPBanUtility;

internal class InfoBarViewModel : ViewModelBase
{
    public InfoBarViewModel(string infoMessage)
    {
        InfoMessage = infoMessage;
    }

    public string InfoMessage { get; }
}
