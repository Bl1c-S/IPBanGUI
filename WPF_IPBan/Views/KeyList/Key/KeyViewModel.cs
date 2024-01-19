using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System;

namespace WPF_IPBanUtility;

internal class KeyViewModel : ViewModelBase
{
    public KeyViewModel(Logic_IPBanUtility.Models.Key key)
    {
        InfoBarVM = new(key.Comment);
        IsInfoBarOpen = false;
        _value = key.Value;

        Key = key;

        IInfoBarCommand = new RelayCommand(InfoBar);
        IPreviousCommand = new RelayCommand(PreviousValue);
        ISaveKeyCommand = new RelayCommand(SaveKey);
        IHideKeyCommand = new RelayCommand(HideKey);
    }
    public event Action<KeyViewModel>? HideKeyEvent;
    public event Action<Logic_IPBanUtility.Models.Key>? SaveKeyEvent;

    public Logic_IPBanUtility.Models.Key Key;
    public string Name => Key.Name;
    private bool IsChanged => _value != Key.Value;

    private string _value;
    public string Value
    {
        get => _value;
        set
        {
            _value = value;
            OnPropertyChanged(nameof(Value));
        }
    }

    #region InfoBar
    public ICommand IInfoBarCommand { get; }
    public InfoBarViewModel? InfoBarVM { get; set; }

    private bool _isInfoBarOpen;
    public bool IsInfoBarOpen
    {
        get => _isInfoBarOpen;
        set
        {
            _isInfoBarOpen = value;

            if (_isInfoBarOpen)
                InfoBarVM = new InfoBarViewModel(Key.Comment);
            else
                InfoBarVM = null;

            OnPropertyChanged(nameof(InfoBarVM));
            OnPropertyChanged(nameof(IsInfoBarOpen));
        }
    }
    private void InfoBar()
    {
        IsInfoBarOpen = !IsInfoBarOpen;
    }
    #endregion

    #region SaveChanged
    public ICommand ISaveKeyCommand { get; }

    private void SaveKey()
    {
        if (!IsChanged)
            return;

        Key.InsertValue(_value);
        SaveKeyEvent?.Invoke(Key);
    }
    #endregion

    #region ReturnPrevious
    public ICommand IPreviousCommand { get; }

    private void PreviousValue()
    {
        Value = Key.Value;
    }
    #endregion

    #region HideKey
    public ICommand IHideKeyCommand { get; }
    private void HideKey()
    {
        HideKeyEvent?.Invoke(this);
    }
    #endregion
}
