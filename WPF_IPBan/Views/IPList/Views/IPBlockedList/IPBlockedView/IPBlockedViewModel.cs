using CommunityToolkit.Mvvm.Input;
using Logic_IPBanUtility.Logic.IPList;
using System;
using System.Collections.ObjectModel;
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

          public ObservableCollection<Button> Buttons { get; }

          public SymbolRegular StatusIcon { get; }
          public string StatusTitle { get; }
          public string Title { get; }
          public string FailedLoginCountMessage { get; }

          public string? BanDateMessage { get; }

          public IPBlockedViewModel(IPAddressEntity ip, Action<IPAddressEntity> addToWhiteList, Action<IPAddressEntity> addToBlackList, Action<IPAddressEntity> remove, Action<IPBlockedViewModel> dispose)
          {
               _iPAddressEntity = ip;
               Title = _iPAddressEntity.IPAddressText;

               (StatusIcon, StatusTitle) = SetStatus(_iPAddressEntity.BanEndDate != null);
               FailedLoginCountMessage = $"{ToolTips.FailedLoginCount} {_iPAddressEntity.FailedLoginCount}";

               BanDateMessage = GetBanDateMessage();

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
          private (SymbolRegular, string) SetStatus(bool isBaned)
          {
               var icon = isBaned ? SymbolRegular.SubtractCircle24 : SymbolRegular.Warning24;
               var title = isBaned ? Status.Ban : Status.Warn;
               return (icon, title);
          }

          private void ExecuteAction(Action<IPAddressEntity> action, Action<IPBlockedViewModel> dispose)
          {
               action.Invoke(_iPAddressEntity);
               dispose.Invoke(this);
          }

          private void Copy() => System.Windows.Clipboard.SetText(Title);

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
                        Margin = new(0, 2,2,2)
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
