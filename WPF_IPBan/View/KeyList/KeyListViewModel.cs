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

     private readonly ConfigFileManager _manager;

     public KeyListViewModel(ConfigFileManager manager)
     {
          _manager = manager;
          _keyViewModels = CreateKeyViewModels(manager.Keys);
     }
     private ObservableCollection<KeyViewModel> CreateKeyViewModels(List<Key> keys)
     {
          ObservableCollection<KeyViewModel> keyViewModels = new();
          foreach (var key in keys)
          {
               if (!key.IsHidden)
                    continue;

               var keyVM = new KeyViewModel(key);
               keyVM.HideKeyEvent += HideKey;
               keyVM.SaveKeyEvent += SaveKey;
               keyViewModels.Add(keyVM);
          }
          return keyViewModels;
     } 
     public void HideKey(KeyViewModel keyViewModel)
     {
          keyViewModel.HideKeyEvent -= HideKey;
          _keyViewModels.Remove(keyViewModel);
          _manager.WriteKeyIdentiChanged(keyViewModel.Key);

          OnPropertyChanged(nameof(KeyViewModels));
     }

     public void SaveKey(Key key)
     {
          _manager.WriteKey(key);
     }
}