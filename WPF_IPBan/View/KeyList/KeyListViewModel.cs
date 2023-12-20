using Logic_IPBanUtility;
using Logic_IPBanUtility.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WPF_IPBanUtility;

internal class KeyListViewModel : ViewModelBase
{
     private ObservableCollection<KeyViewModel> _keyViewModels;
     public ObservableCollection<KeyViewModel> KeyViewModels
     {
          get => _keyViewModels; private set
          {
               _keyViewModels = value;
               OnPropertyChanged(nameof(KeyViewModels));
          }
     }

     private readonly ConfigFileManager _cfgManager;

     public KeyListViewModel(ConfigFileManager cfgManager)
     {
          _cfgManager = cfgManager;
          _keyViewModels = CreateKeyViewModels(cfgManager.CreateKeys());
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
          if (!key.IsHidden)
               return null;

          var keyVM = new KeyViewModel(key);
          keyVM.HideKeyEvent += HideKey;
          keyVM.SaveKeyEvent += SaveKey;
          return keyVM;
     }
     public void HideKey(KeyViewModel keyViewModel)
     {
          keyViewModel.HideKeyEvent -= HideKey;
          _keyViewModels.Remove(keyViewModel);

          var keyIdenti = keyViewModel.Key.KeyIdenti;
          keyIdenti.IsHidden = false;
          _cfgManager.WriteKeyIdentiChanged(keyViewModel.Key.KeyIdenti);

          OnPropertyChanged(nameof(KeyViewModels));
     }

     public void SaveKey(Key key)
     {
          _cfgManager.WriteKey(key);
     }
}