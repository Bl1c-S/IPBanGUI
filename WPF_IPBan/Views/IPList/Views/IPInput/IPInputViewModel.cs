using WPF_IPBanUtility.Properties;
using System;
using Logic_IPBanUtility.Models;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.RegularExpressions;
using System.Linq;
using static WPF_IPBanUtility.Views.IPList.IPListView.View.IPListVMsBuilder;

namespace WPF_IPBanUtility;

public class IPInputViewModel : ViewModelBase
{
     public IPValidation IPValidator { get; set; } = new();
     public ICommand AddNewIPCommand { get; }
     private Func<KeyNames, string, bool> _addNewIP;
     private readonly Action<KeyNames> _iPListChanged;

     private KeyNames _selectedKey;
     private string _selectedEndText;
     private string _selectedItem;
     public string SelectedItem
     {
          get => _selectedItem;
          set
          {
               _selectedItem = value;
               SetKeyName();
               OnPropertyChanged(nameof(SelectedItem));
          }
     }
     public string WhiteList { get; private set; }
     public string BlackList { get; private set; }
     public string AddText { get => ButtonNames.Add; }

     private void SetKeyName()
     {
          if (_selectedItem.Contains(WhiteList))
          {
               _selectedKey = KeyNames.Whitelist;
               _selectedEndText = ToolTips.ToWiteList;
          }
          else if (_selectedItem.Contains(BlackList))
          {
               _selectedKey = KeyNames.Blacklist;
               _selectedEndText = ToolTips.ToBlackList;
          }
     }

     public IPInputViewModel(Func<KeyNames, string, bool> addNewIP, Action<KeyNames> iPListChanged)
     {
          _addNewIP = addNewIP;
          _iPListChanged = iPListChanged;
          AddNewIPCommand = new RelayCommand(AddNewIP);

          WhiteList = $"{ButtonNames.Add} {ToolTips.ToWiteList}";
          BlackList = $"{ButtonNames.Add} {ToolTips.ToBlackList}";

          _selectedItem = WhiteList;
          _selectedEndText = ToolTips.ToWiteList;
          SetKeyName();
     }

     public void AddNewIP()
     {
          var result = _addNewIP.Invoke(_selectedKey, IPValidator.IpAddress);
          if (result)
          {
               _iPListChanged.Invoke(_selectedKey);
               DialogMessageBox.InfoBox(Status.Successfully, $"Адресу {IPValidator.IpAddress} додано {_selectedEndText}");
          }
          else DialogMessageBox.InfoBox(Status.Error, $"Адреса {IPValidator.IpAddress} вже було додано {_selectedEndText}");
     }

     public class IPValidation : ObservableObject
     {
          private const string PATTERN = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

          public string CollorField { get; private set; } = EmptyCollor;
          private const string ReadCollor = "#B2FF2222";
          private const string EmptyCollor = "#00000000";

          private bool _isValidIP = false;
          public bool IsValidIP
          {
               get => _isValidIP; private set
               {
                    _isValidIP = value;
                    OnPropertyChanged(nameof(IsValidIP));
                    CollorField = _isValidIP ? EmptyCollor : ReadCollor;
                    OnPropertyChanged(nameof(CollorField));
               }
          }
          private void IPValidate(string ip)
          {
               _ipAddress = ip;
               IsValidIP = RegecxValidate(ip);
               OnPropertyChanged(nameof(IpAddress));
          }
          public string ToolTip { get => ToolTips.IncorrectIPAddress; }

          private string _ipAddress = string.Empty;
          public string IpAddress { get => _ipAddress; set { IPValidate(value); } }
          private bool RegecxValidate(string ip)
          {
               if (Regex.IsMatch(ip, PATTERN))
                    return true;
               return false;
          }
     }
}