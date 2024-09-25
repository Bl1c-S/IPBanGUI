using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility;
using Logic_IPBanUtility.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WPF_IPBanUtility.Properties;
using Key = Logic_IPBanUtility.Models.Key;

namespace WPF_IPBanUtility;

public class KeyListViewModel : PageViewModelBase
{
     private readonly ConfigFileManager _cfgManager;
     private readonly WinServicesController _servicesController;

     public KeyListViewModel(ConfigFileManager cfgManager, WinServicesController servicesController) : base(PageNames.KeyList)
     {
          _cfgManager = cfgManager;
          _servicesController = servicesController;
          _keyViewModels = CreateKeyViewModels(cfgManager.CreateKeys());

          ISaveAllCommand = new RelayCommand(SaveAllKey);
          IReturnAllPreviousValueCommand = new RelayCommand(ReturnAllPreviousValue);
          CreatePageButtons();
     }

     #region Changed
     public string BorderCollor { get; private set; } = Collors.InActive;
     private bool _allKeySaved = true;

     public override void ApplyChanges(ApplyOptions[]? options = null)
     {
          if (!_allKeySaved)
          {
               var res = MessageBox.Show(Messages.KeysChanged, PageNames.KeysChanged, MessageBoxButton.YesNo, MessageBoxImage.Information);
               if (res == MessageBoxResult.Yes)
                    SaveAllKey();
               else
                    ReturnAllPreviousValue();
          }
          if (PageHaveChanges)
          {
               if (options != null && options.Contains(ApplyOptions.Await))
               {
                    _servicesController.IPBan.Restart().Wait();
               }
               else
               {
                    _servicesController.IPBan.Restart();
               }
          }
          PageHaveChanges = false;
     }
     protected override void PageChanged()
     {
          base.PageChanged();
          ChangeInfoVisibility(PageHaveChanges);
     }
     private void SaveAllButtonEnableChanged()
     {
          PageChanged();
          var changedVM = _keyViewModels.FirstOrDefault(vm => vm.IsChanged == true);
          _allKeySaved = changedVM != null ? false : true;
          var saveButton = PageButtons.FirstOrDefault(b => b.Icon == Wpf.Ui.Common.SymbolRegular.SaveMultiple24);
          var borderColor = (Color)ColorConverter.ConvertFromString(_allKeySaved ? Collors.InActive : Collors.Active);
          saveButton!.BorderBrush = new SolidColorBrush(borderColor);
     }
     #endregion

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

          var keyVM = new KeyViewModel(key, SaveKey, HideKey, SaveAllButtonEnableChanged);
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
               var isChanged = key.SetValue(keyVM.Value);
               if (isChanged)
               {
                    keys.Add(key);
                    keyVM.CheckChanges();
               }
          }
          _cfgManager.WriteKeys(keys);
          SaveAllButtonEnableChanged();
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
          base.CreatePageButtons();
          ChangeInfoMessage(ToolTips.ReloadIPBanService);

          PageButtons.Add(CreateButtonWithTitle(
               ISaveAllCommand, Wpf.Ui.Common.SymbolRegular.SaveMultiple24, ButtonNames.SaveAll));
          PageButtons.Add(CreateButtonWithTitle(
               IReturnAllPreviousValueCommand, Wpf.Ui.Common.SymbolRegular.ArrowReplyAll24,
               ButtonNames.ReturnAll, "", new(4, 0, 0, 0)));
     }

     #region Dispose
     public override void Dispose()
     {
          ApplyChanges(new[] { ApplyOptions.Await });

          foreach (var keyVM in _keyViewModels)
               KeyVMDispose(keyVM);

          base.Dispose();
     }
     private void KeyVMDispose(KeyViewModel keyVM)
     {
          keyVM.SaveKeyEvent -= SaveKey;
          keyVM.HideKeyEvent -= HideKey;
          keyVM.IsChangedChange -= SaveAllButtonEnableChanged;
          keyVM.Dispose();
     }
     #endregion
}