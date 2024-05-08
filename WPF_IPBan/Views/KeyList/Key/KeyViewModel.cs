using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows;
using Logic_IPBanUtility.Models;
using System.Windows.Input;
using Key = Logic_IPBanUtility.Models.Key;

namespace WPF_IPBanUtility;

public class KeyViewModel : ViewModelBase
{
     public KeyViewModel(Key key, Action<Key> saveKey, Action<KeyViewModel> hideKey)
     {
          _value = key.Value;
          Key = key;

          SaveKeyEvent += saveKey;
          HideKeyEvent += hideKey;

          IDescriptionVisibilityChangeCommand = new RelayCommand(DescriptionVisibilityChange);
          IPreviousCommand = new RelayCommand(PreviousValue);
          ISaveKeyCommand = new RelayCommand(SaveKey);
          IHideKeyCommand = new RelayCommand(HideKey);
     }

     public Key Key;
     public string Name => Key.Name;
     public string Description => Key.Description;

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
               SaveKeyEvent.Invoke(Key);
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
