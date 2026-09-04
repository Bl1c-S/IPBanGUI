using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using Key = Logic_IPBanUtility.Models.Key;
using WPF_IPBanUtility.Properties;
using Wpf.Ui.Controls;

namespace WPF_IPBanUtility;

public class KeyViewModel : ViewModelBase
{
     public Key Key { get; }

     public ObservableCollection<Button> Buttons { get; } = new();

     public string Value
     {
          get => _value;
          set
          {
               _value = value;
               CheckChanges();
               OnPropertyChanged();
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

          Buttons.Add(CreateButton(new RelayCommand(DescriptionVisibilityChange),
               SymbolRegular.QuestionCircle24, ToolTips.Description, new(4, 0, 0, 0))
          );

          Buttons.Add(CreateButton(new RelayCommand(SaveKey),
               SymbolRegular.Save28, ToolTips.SaveChanges, new(4, 0, 0, 0))
          );

          Buttons.Add(CreateButton(new RelayCommand(PreviousValue),
               SymbolRegular.ArrowHookUpLeft24, ToolTips.RevertChanges, new(4, 0, 0, 0))
          );

          Buttons.Add(CreateButton(new RelayCommand(HideKey),
               SymbolRegular.EyeOff24, ToolTips.HideKey, new(4, 0, 0, 0))
          );
     }

     #region Changed

     public event Action IsChangedChange;
     public string BorderCollor { get; private set; } = Collors.InActive;

     private bool _isChanged;

     public bool IsChanged
     {
          get => _isChanged;
          private set
          {
               if (_isChanged != value)
               {
                    _isChanged = value;
                    IsChangedChange.Invoke();
                    BorderCollor = _isChanged ? Collors.Active : Collors.InActive;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(BorderCollor));
               }
          }
     }

     public void CheckChanges() => IsChanged = Key.Value != _value;

     #endregion

     public Visibility DescriptionVisibility { get; set; } = Visibility.Collapsed;

     private void DescriptionVisibilityChange()
     {
          if (DescriptionVisibility != Visibility.Visible)
               DescriptionVisibility = Visibility.Visible;
          else
               DescriptionVisibility = Visibility.Collapsed;
          OnPropertyChanged(nameof(DescriptionVisibility));
     }

     public event Action<Key> SaveKeyEvent;

     public void SaveKey()
     {
          if (Key.SetValue(_value))
          {
               SaveKeyEvent.Invoke(Key);
               CheckChanges();
          }
     }

     public void PreviousValue() => Value = Key.Value;

     public event Action<KeyViewModel> HideKeyEvent;

     private void HideKey()
     {
          HideKeyEvent.Invoke(this);
     }
}