using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;

namespace WPF_IPBanUtility;

internal class KeyViewModel : ViewModelBase
{
     public event Action<KeyViewModel>? KeyListChanged;
     public KeyViewModel(Logic_IPBanUtility.Key key)
     {
          InfoBarVM = new(key.Comment);
          IsInfoBarOpen = false;
          _value = key.Value;

          _key = key;

          IInfoBarOpenCommand = new RelayCommand(ChangeInfoBarOpen);
          ISaveChangedCommand = new RelayCommand(SaveChanged);
          IReturnPreviousCommand = new RelayCommand(ReturnPreviousValue);
          IHideKeyCommand = new RelayCommand(HideKey);

     }

     private Logic_IPBanUtility.Key _key;
     public string Name => _key.Name;
     private bool IsChanged => _value != _key.Value;

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
     public ICommand IInfoBarOpenCommand { get; }
     public InfoBarViewModel? InfoBarVM { get; set; }

     private bool _isInfoBarOpen;
     public bool IsInfoBarOpen
     {
          get => _isInfoBarOpen;
          set
          {
               _isInfoBarOpen = value;

               if (_isInfoBarOpen)
                    InfoBarVM = new InfoBarViewModel(_key.Comment);
               else
                    InfoBarVM = null;

               OnPropertyChanged(nameof(InfoBarVM));
               OnPropertyChanged(nameof(IsInfoBarOpen));
          }
     }
     private void ChangeInfoBarOpen()
     {
          IsInfoBarOpen = !IsInfoBarOpen;
     }
     #endregion

     #region SaveChanged
     public ICommand ISaveChangedCommand { get; }

     private void SaveChanged()
     {
          if (IsChanged)
               _key.InsertValue(_value);
     }
     #endregion

     #region ReturnPrevious
     public ICommand IReturnPreviousCommand { get; }

     private void ReturnPreviousValue()
     {
          Value = _key.Value;
     }
     #endregion

     #region HideKey
     public ICommand IHideKeyCommand { get; }
     private void HideKey()
     {
          KeyListChanged?.Invoke(this);
     }
     #endregion
}
