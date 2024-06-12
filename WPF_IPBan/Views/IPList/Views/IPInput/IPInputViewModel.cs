using WPF_IPBanUtility.Properties;
using System;
using Logic_IPBanUtility.Models;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.RegularExpressions;

namespace WPF_IPBanUtility;

public class IPInputViewModel : ViewModelBase
{
     public IPValidation IPValidator { get; set; }
     public ICommand AddNewIPCommand { get; }
     private Func<KeyNames, string[]> _getIPs { get; }
     private Action<KeyNames, string> _addNewIP;
     private readonly Action<KeyNames> _iPListChanged;

     public Action? SelectedItemChanged;
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
               SelectedItemChanged?.Invoke();
               OnPropertyChanged(nameof(SelectedItem));
          }
     }
     public string WhiteListText { get; private set; }
     public string BlackListText { get; private set; }
     public string AddText { get => ButtonNames.Add; }

     private void SetKeyName()
     {
          if (_selectedItem.Contains(WhiteListText))
          {
               _selectedKey = KeyNames.Whitelist;
               _selectedEndText = ToolTips.ToWiteList;
          }
          else if (_selectedItem.Contains(BlackListText))
          {
               _selectedKey = KeyNames.Blacklist;
               _selectedEndText = ToolTips.ToBlackList;
          }
     }

     public IPInputViewModel(Action<KeyNames, string> addNewIP, Func<KeyNames, string[]> getIPs, Action<KeyNames> iPListChanged)
     {
          _addNewIP = addNewIP;
          _getIPs = getIPs;
          _iPListChanged = iPListChanged;

          AddNewIPCommand = new RelayCommand(AddNewIP);

          WhiteListText = $"{ButtonNames.Add} {ToolTips.ToWiteList}";
          BlackListText = $"{ButtonNames.Add} {ToolTips.ToBlackList}";

          _selectedItem = WhiteListText;
          _selectedEndText = ToolTips.ToWiteList;
          SetKeyName();
          IPValidator = new(_getIPs, () => _selectedKey, () => _selectedEndText);
          SelectedItemChanged += IPValidator.IPValidate;
     }

     public void AddNewIP()
     {
          _addNewIP.Invoke(_selectedKey, IPValidator.IpAddress);
          _iPListChanged.Invoke(_selectedKey);
          IPValidator.IpAddress = string.Empty;
     }

     public class IPValidation : ObservableObject
     {
          private string _collorField = Collors.EmptyCollor;
          public string CollorField
          {
               get => _collorField; private set
               {
                    _collorField = value;
                    OnPropertyChanged(nameof(CollorField));
               }
          }

          private bool _isValidIP = false;
          public bool IsValidIP
          {
               get => _isValidIP; private set
               {
                    _isValidIP = value;
                    OnPropertyChanged(nameof(IsValidIP));
               }
          }

          public string _toolTip = ToolTips.IncorrectIPAddress;
          public string ToolTip
          {
               get => _toolTip; private set
               {
                    _toolTip = value;
                    OnPropertyChanged(nameof(ToolTip));
               }
          }

          public string IpAddress
          {
               get => _ipAddress; set
               {
                    _ipAddress = value;
                    IPValidate();
                    OnPropertyChanged(nameof(IpAddress));
               }
          }
          private string _ipAddress = string.Empty;
          private readonly Func<KeyNames, string[]> _getIPs;
          private readonly Func<KeyNames> _selectedKey;
          private readonly Func<string> _selectedEndText;

          public IPValidation(Func<KeyNames, string[]> getIPs, Func<KeyNames> selectedKey, Func<string> selectedEndText)
          {
               _getIPs = getIPs;
               _selectedKey = selectedKey;
               _selectedEndText = selectedEndText;
          }

          public void IPValidate()
          {
               if (_ipAddress == string.Empty)
               {
                    IsValidIP = false;
                    CollorField = Collors.EmptyCollor;
                    ToolTip = ToolTips.IncorrectIPAddress;
               }
               else if (!RegecxCurrentIpFormatValidate(_ipAddress))
               {
                    IsValidIP = false;
                    CollorField = Collors.ReadCollor;
                    ToolTip = ToolTips.IncorrectIPAddress;
               }
               else if (IsNewIP(_ipAddress))
               {
                    IsValidIP = true;
                    CollorField = Collors.GreanCollor;
                    ToolTip = $"{_ipAddress} {ToolTips.CurrentIPAddress} {_selectedEndText.Invoke()}";
               }
               else
               {
                    IsValidIP = false;
                    CollorField = Collors.YelowCollor;
                    ToolTip = $"{_ipAddress} {ToolTips.IPAddressExist} {_selectedEndText.Invoke()}";
               }
          }
          private bool IsNewIP(string ip)
          {
               var ips = _getIPs.Invoke(_selectedKey.Invoke());
               foreach (var ipL in ips)
                    if (ipL.Equals(ip)) return false;
               return true;
          }

          private const string PATTERN = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
          private bool RegecxCurrentIpFormatValidate(string ip)
          {
               if (Regex.IsMatch(ip, PATTERN))
                    return true;
               return false;
          }
     }
}