using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using Wpf.Ui.Controls;
using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;
using ContextMenu = System.Windows.Controls.ContextMenu;
using MenuItem = System.Windows.Controls.MenuItem;
using PlacementMode = System.Windows.Controls.Primitives.PlacementMode;

namespace WPF_IPBanUtility;

public abstract class IPListViewModelBase : ViewModelBase
{
     public string Title => Properties.Title;
     public IPListViewProperties Properties { get; }

     public Action? VmChanged;

     public string ItemCountText => $"{OtherWords.Total} {ItemCount}";
     public int ItemCount { get; private set; }
     public bool HasItems => ItemCount > 0;

     public ObservableCollection<IPUserControlViewModelBase> VMs { get; protected set; } = [];
     public ObservableCollection<Button> Buttons { get; } = [];

     private IRelayCommand RemoveAllCommand { get; }

     private readonly Button _removeAllButton;

     protected IPListViewModelBase(IPListViewProperties properties)
     {
          Properties = properties;

          RemoveAllCommand = new RelayCommand(RemoveAll, () => HasItems);

          _removeAllButton = CreateListHeaderButton(
               new RelayCommand(OpenClearConfirmMenu),
               SymbolRegular.Delete20,
               ToolTips.RemoveFromList);

          Buttons.Add(_removeAllButton);
     }

     public abstract void RemoveAll();
     public void ClearAll() => RemoveAll();

     private void OpenClearConfirmMenu()
     {
          _removeAllButton.ContextMenu ??= CreateContextMenuConfirmation(
               ToolTips.RemoveFromList,
               RemoveAllCommand,
               SymbolRegular.Delete20);

          OpenContextMenu(_removeAllButton);
     }

     protected void OpenContextMenu(Button button, ContextMenu? contextMenu = null)
     {
          if (contextMenu != null)
               button.ContextMenu = contextMenu;

          if (button.ContextMenu is null)
               return;

          button.ContextMenu.PlacementTarget = button;
          button.ContextMenu.Placement = PlacementMode.Bottom;
          button.ContextMenu.IsOpen = true;
     }

     protected Button CreateListHeaderButton(IRelayCommand command, SymbolRegular icon, string toolTip)
     {
          var button = CreateButton(
               command,
               icon,
               toolTip,
               new Thickness(6, 4, 6, 4));

          button.VerticalAlignment = VerticalAlignment.Center;
          button.IsEnabled = HasItems;
          return button;
     }

     protected ContextMenu CreateContextMenuConfirmation(string warningText,
          IRelayCommand command, SymbolRegular icon)
     {
          var menu = new ContextMenu { Width = 250 };

          menu.Items.Add(new MenuItem
          {
               IsEnabled = false,
               Header = new TextBlock
               {
                    Text = warningText,
                    TextWrapping = TextWrapping.Wrap
               }
          });
          menu.Items.Add(new MenuItem
          {
               Header = ButtonNames.Confirm,
               Command = command,
               Icon = new SymbolIcon(icon),
               Margin = new Thickness(0, 0, 8, 0)
          });
          return menu;
     }

     protected void DisposeItem(IPUserControlViewModelBase vm)
     {
          VMs.Remove(vm);
          vm.Dispose();
     }

     protected virtual void IpListChanged(bool currentVmChanged = false)
     {
          ItemCount = VMs.Count;
          OnPropertyChanged(nameof(VMs));
          OnPropertyChanged(nameof(ItemCountText));
          OnPropertyChanged(nameof(HasItems));
          RemoveAllCommand.NotifyCanExecuteChanged();

          foreach (var b in Buttons)
               b.IsEnabled = HasItems;

          if (currentVmChanged)
               VmChanged?.Invoke();
     }

     public void Update()
     {
     }
}
