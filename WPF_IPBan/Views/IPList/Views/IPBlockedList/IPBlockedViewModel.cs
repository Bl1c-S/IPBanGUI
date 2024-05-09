using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility.Logic.IPList;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using WPF_IPBanUtility.Properties;

namespace WPF_IPBanUtility.Views.IPList.Views.IPBlockedList
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

               IAddToWiteListCommand = new RelayCommand(() => ExecuteAction(addToWhiteList, dispose));
               IAddToBlackListCommand = new RelayCommand(() => ExecuteAction(addToBlackList, dispose));
               IRemoveCommand = new RelayCommand(() => ExecuteAction(remove, dispose));
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

          private void ExecuteAction(Action<IPAddressEntity> action, Action<IPBlockedViewModel> dispose)
          {
               try
               {
                    action.Invoke(_iPAddressEntity);
                    dispose.Invoke(this);
               }
               catch (Exception ex)
               {
                    var copyError = () => { System.Windows.Clipboard.SetText(ex.Message); };
                    DialogMessageBox.ActionBox(copyError, Messages.Error, $"{ ex.Message}\r\n{ex.InnerException}", ButtonNames.Copy);
               }
          }

          private ObservableCollection<Button> CreateButtons()
          {
               Thickness margin = new(2);
               return new()
               {
                   new Button
                   {
                        Command = ICopyCommand,
                        Icon = SymbolRegular.Copy20,
                        ToolTip = ToolTips.Copy,
                        Margin = margin
                   },
                   new Button
                   {
                        Command = IAddToWiteListCommand,
                        Icon = SymbolRegular.CheckmarkCircle20,
                        ToolTip = ToolTips.AddToWiteList,
                        Margin = margin
                   },
                   new Button
                   {
                        Command = IRemoveCommand,
                        Icon = SymbolRegular.Delete20,
                        ToolTip = ToolTips.RemoveBlockList,
                        Margin = margin
                   },
                   new Button
                   {
                        Command = IAddToBlackListCommand,
                        Icon = SymbolRegular.Prohibited20,
                        ToolTip = ToolTips.AddToBlackList,
                        Margin = margin
                   }
               };
          }

          public ICommand IAddToWiteListCommand { get; }
          public ICommand IAddToBlackListCommand { get; }
          public ICommand IRemoveCommand { get; }
          public ICommand ICopyCommand { get; }
     }
}
