using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility.Logic.IPList;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using WPF_IPBanUtility.Properties;

namespace WPF_IPBanUtility
{
     public class IPBlockedViewModel : IPUserControlViewModelBase
     {
          private const string format = "MM.dd  HH:mm:ss";
          private readonly IPAddressEntity _iPAddressEntity;

          public IPBlockedViewModel(IPAddressEntity ip, Action<IPAddressEntity> addToWhiteList, Action<IPAddressEntity> addToBlackList, Action<IPAddressEntity> remove, Action<IPBlockedViewModel> dispose) :
               base(ip.IPAddressText)
          {
               _iPAddressEntity = ip;
               SetStatus(_iPAddressEntity.BanEndDate != null);
               Message = GetBanDateMessage();

               IAddToWiteListCommand = new AsyncRelayCommand(() => ExecuteAction(addToWhiteList, dispose));
               IAddToBlackListCommand = new AsyncRelayCommand(() => ExecuteAction(addToBlackList, dispose));
               IRemoveCommand = new AsyncRelayCommand(() => ExecuteAction(remove, dispose));
               ICopyCommand = new RelayCommand(Copy);

               Buttons = CreateButtons();
          }
          private string GetBanDateMessage()
          {
               var banDate = _iPAddressEntity.BanDate?.ToString(format);
               var banEndDate = _iPAddressEntity.BanEndDate?.ToString(format);

               if (banDate == null) return string.Empty;
               else return $"{banDate} - {banEndDate}";
          }
          private void SetStatus(bool isBaned)
          {
               var icon = isBaned ? SymbolRegular.Timer24 : SymbolRegular.Warning24;
               var title = isBaned ? Properties.Status.Ban : Properties.Status.Warn;
               var message = $"{ToolTips.FailedLoginCount} {_iPAddressEntity.FailedLoginCount}";
               Status = new(icon, title, message);
               base.SetStatus();
          }

          private Task ExecuteAction(Action<IPAddressEntity> action, Action<IPBlockedViewModel> dispose)
          {
               return Task.Run(() =>
               {
                    try
                    {
                         Application.Current.Dispatcher.Invoke(() =>
                         {
                              action.Invoke(_iPAddressEntity);
                              dispose.Invoke(this);
                         });
                    }
                    catch (Exception ex)
                    {
                         var copyError = () => { System.Windows.Clipboard.SetText(ex.Message); };
                         DialogMessageBox.ActionBox(copyError, Properties.Status.Error, $"{ex.Message}\r\n{ex.InnerException}", ButtonNames.Copy);
                    }
               });
          }

          private ObservableCollection<Button> CreateButtons()
          {
               Thickness margin = new(2);
               return new()
               {
                   CreateButton(ICopyCommand, SymbolRegular.Copy20,  ToolTips.Copy, margin),
                   CreateButton(IAddToWiteListCommand, SymbolRegular.CheckmarkCircle20, ToolTips.ToWiteList, margin),
                   CreateButton(IRemoveCommand, SymbolRegular.Delete20,ToolTips.RemoveBlockList,margin),
                   CreateButton(IAddToBlackListCommand, SymbolRegular.Prohibited20, ToolTips.ToBlackList, margin)
               };
          }

          public ICommand IAddToWiteListCommand { get; }
          public ICommand IAddToBlackListCommand { get; }
          public ICommand IRemoveCommand { get; }
          public ICommand ICopyCommand { get; }
     }
}
