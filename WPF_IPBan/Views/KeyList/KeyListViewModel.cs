using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPF_IPBanUtility.Properties;
using Button = Wpf.Ui.Controls.Button;
using Key = Logic_IPBanUtility.Models.Key;

namespace WPF_IPBanUtility;

public class KeyListViewModel : PageViewModelBase
{
     private readonly ConfigFileManager _cfgManager;

     public KeyListViewModel(ConfigFileManager cfgManager) : base(PageNames.KeyList)
     {
          _cfgManager = cfgManager;
          _keyViewModels = CreateKeyViewModels(cfgManager.CreateKeys());

          ISaveAllCommand = new RelayCommand(SaveAllKey);
          IReturnAllPreviousValueCommand = new RelayCommand(ReturnAllPreviousValue);
          CreatePageButtons();
     }

     #region KeyViewModel
     private ObservableCollection<KeyViewModel> _keyViewModels;
     public ObservableCollection<KeyViewModel> KeyViewModels
     {
          get => _keyViewModels; private set
          {
               _keyViewModels = value;
               OnPropertyChanged(nameof(KeyViewModels));
          }
     }
     private ObservableCollection<KeyViewModel> CreateKeyViewModels(List<Key> keys)
     {
          ObservableCollection<KeyViewModel> keyViewModels = new();
          foreach (var key in keys)
          {
               var keyVM = CreateKeyViewModel(key);
               if (keyVM != null)
                    keyViewModels.Add(keyVM);
          }
          return keyViewModels;
     }
     private KeyViewModel? CreateKeyViewModel(Key key)
     {
          if (!key.IsHidden) return null;

          var keyVM = new KeyViewModel(key, SaveKey, HideKey);
          return keyVM;
     }
     #endregion

     #region Hide
     public void HideKey(KeyViewModel keyViewModel)
     {
          _keyViewModels.Remove(keyViewModel);
          KeyVMDispose(keyViewModel);

          var keyIdenti = keyViewModel.Key.KeyIdenti;
          keyIdenti.IsHidden = false;
          _cfgManager.WriteKeyIdentiChanged(keyViewModel.Key.KeyIdenti);

          OnPropertyChanged(nameof(KeyViewModels));
     }
     #endregion

     #region Save
     private void SaveKey(Key key)
     {
          _cfgManager.WriteKey(key);
     }
     public ICommand ISaveAllCommand { get; }
     private void SaveAllKey()
     {
          var keys = new List<Key>();
          foreach (var keyVM in _keyViewModels)
          {
               var key = keyVM.Key;
               var isChanged = key.InsertValue(keyVM.Value);
               if (isChanged)
                    keys.Add(key);
          }
          _cfgManager.WriteKeys(keys);
     }
     #endregion

     #region Previous
     public ICommand IReturnAllPreviousValueCommand { get; }
     private void ReturnAllPreviousValue()
     {
          foreach (var keyVM in _keyViewModels)
               keyVM.PreviousValue();
     }

     #endregion

     protected override void CreatePageButtons()
     {
          PageButtons.Add(new Button
          {
               Content = ButtonNames.SaveAll,
               Command = ISaveAllCommand,
               Icon = Wpf.Ui.Common.SymbolRegular.SaveMultiple24
          });

          PageButtons.Add(new Button
          {
               Content = ButtonNames.ReturnAll,
               Command = IReturnAllPreviousValueCommand,
               Icon = Wpf.Ui.Common.SymbolRegular.ArrowReplyAll24,
               Margin = new(4, 0, 0, 0)
          });
     }

     #region Dispose
     public override void Dispose()
     {
          foreach (var keyVM in _keyViewModels)
               KeyVMDispose(keyVM);

          base.Dispose();
     }
     private void KeyVMDispose(KeyViewModel keyVM)
     {
          keyVM.SaveKeyEvent -= SaveKey;
          keyVM.HideKeyEvent -= HideKey;
          keyVM.Dispose();
     }
     #endregion
}