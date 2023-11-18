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
     public void KeyListChanged(KeyViewModel keyViewModel)
     {
          keyViewModel.KeyListChanged -= KeyListChanged;
          _keyViewModels.Remove(keyViewModel);
          OnPropertyChanged(nameof(KeyViewModels));
     }

     private ObservableCollection<KeyViewModel> CreateKeyViewModels(List<Key> keys)
     {
          ObservableCollection<KeyViewModel> keyViewModels = new();
          foreach (var key in keys)
          {
               var keyVM = new KeyViewModel(key);
               keyVM.KeyListChanged += KeyListChanged;
               keyViewModels.Add(keyVM);
          }
          return keyViewModels;
     }
}
