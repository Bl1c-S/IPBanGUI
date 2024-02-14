using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System;
using System.Windows;

namespace WPF_IPBanUtility;

internal class KeyViewModel : ViewModelBase
{
     public KeyViewModel(Logic_IPBanUtility.Models.Key key)
     {
          _value = key.Value;
          Key = key;

          IDescVisibilityChangeCommand = new RelayCommand(DescriptionVisibilityChange);
          IPreviousCommand = new RelayCommand(PreviousValue);
          ISaveKeyCommand = new RelayCommand(SaveKey);
          IHideKeyCommand = new RelayCommand(HideKey);
     }
     public event Action<KeyViewModel>? HideKeyEvent;
     public event Action<Logic_IPBanUtility.Models.Key>? SaveKeyEvent;

     public Logic_IPBanUtility.Models.Key Key;
     public string Name => Key.Name;
     public string Description => Key.Description;
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
     public ICommand IDescVisibilityChangeCommand { get; }
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
