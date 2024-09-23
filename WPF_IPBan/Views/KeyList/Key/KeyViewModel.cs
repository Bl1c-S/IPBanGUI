using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows;
using System.Windows.Input;
using Key = Logic_IPBanUtility.Models.Key;
using WPF_IPBanUtility.Properties;

namespace WPF_IPBanUtility;

public class KeyViewModel : ViewModelBase
{
     public Key Key;
     public string Name => Key.Name;
     public string Description => Key.Description;

     public string Value
     {
          get => _value;
          set
          {
               _value = value;
               CheckChanges();
               OnPropertyChanged(nameof(Value));
          }
     }
     private string _value;

     public KeyViewModel(Key key, Action<Key> saveKey, Action<KeyViewModel> hideKey, Action isChangedChange)
     {
          _value = key.Value;
          Key = key;
          IsChangedChange += isChangedChange;
          SaveKeyEvent += saveKey;
          HideKeyEvent += hideKey;

          IDescriptionVisibilityChangeCommand = new RelayCommand(DescriptionVisibilityChange);
          IPreviousCommand = new RelayCommand(PreviousValue);
          ISaveKeyCommand = new RelayCommand(SaveKey);
          IHideKeyCommand = new RelayCommand(HideKey);
     }

     #region Changed
     public event Action IsChangedChange;
     public string BorderCollor { get; private set; } = Collors.InActive;

     private bool _isChanged = false;
     public bool IsChanged
     {
          get => _isChanged; private set
          {
               if (_isChanged != value)
               {
                    _isChanged = value;
                    IsChangedChange.Invoke();
                    BorderCollor = _isChanged ? Collors.Active : Collors.InActive;
                    OnPropertyChanged(nameof(IsChanged));
                    OnPropertyChanged(nameof(BorderCollor));
               }
          }
     }
     public void CheckChanges() => IsChanged = Key.Value != _value;
     #endregion

     #region Description
     public ICommand IDescriptionVisibilityChangeCommand { get; }
     public Visibility DescriptionVisibility { get; set; } = Visibility.Collapsed;
     private void DescriptionVisibilityChange()
     {
          if (DescriptionVisibility != Visibility.Visible)
               DescriptionVisibility = Visibility.Visible;
          else
               DescriptionVisibility = Visibility.Collapsed;
          OnPropertyChanged(nameof(DescriptionVisibility));
     }
     #endregion

     #region SaveChanged
     public ICommand ISaveKeyCommand { get; }
     public event Action<Key> SaveKeyEvent;

     public void SaveKey()
     {
          if (Key.SetValue(_value))
          {
               SaveKeyEvent.Invoke(Key);
               CheckChanges();
          }
     }
     #endregion

     #region ReturnPrevious
     public ICommand IPreviousCommand { get; }

     public void PreviousValue()
     {
          Value = Key.Value;
     }
     #endregion

     #region HideKey
     public ICommand IHideKeyCommand { get; }
     public event Action<KeyViewModel> HideKeyEvent;
     private void HideKey()
     {
          HideKeyEvent?.Invoke(this);
     }
     #endregion
}
