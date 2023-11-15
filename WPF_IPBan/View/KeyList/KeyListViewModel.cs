using Logic_IPBanUtility;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace WPF_IPBanUtility;

internal class KeyListViewModel : ViewModelBase
{
     public List<KeyViewModel> KeyViewModels { get; set; }
     private readonly ConfigFileManager _manager;

     public KeyListViewModel(ConfigFileManager manager)
     {
          _manager = manager;
          KeyViewModels = CreateKeyViewModels(manager.Keys);
          OnPropertyChanged(nameof(KeyViewModels));
     }
     
     private List<KeyViewModel> CreateKeyViewModels(List<Key> keys)
     {
          List<KeyViewModel> keyViewModels = new();
          foreach (var key in keys)
               keyViewModels.Add(new(key));
          return keyViewModels;
     }
}
