using Logic_IPBanUtility.Logic.IPList;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WPF_IPBanUtility.Properties;
using WPF_IPBanUtility.Views.IPList;

namespace WPF_IPBanUtility;
public class IPBlockedListViewModel : IPListViewModelBase
{
    private readonly IPBlockedListService _iPBlokedListService;

    public IPBlockedListViewModel(IPBlockedListService iPListService, IPListViewProperties properties) : base(PageNames.BlockList, iPListService.IPs.Count, properties)
    {
        _iPBlokedListService = iPListService;
        VMs = BuildVMs(iPListService.IPs);
        _iPBlokedListService.IPsChanged += IPListChanged;
    }

    private ObservableCollection<IPUserControlViewModelBase> BuildVMs(List<IPAddressEntity> ips)
    {
        return new(ips.Select(CreateVM));
    }
    private IPBlockedViewModel CreateVM(IPAddressEntity iPAddressEntity)
    {
        return new(iPAddressEntity,
             _iPBlokedListService.AddToWhiteList,
             _iPBlokedListService.AddToBlacklist,
             _iPBlokedListService.Remove,
             DisposeItem);
    }

    protected override void IPListChanged()
    {
        VMs = BuildVMs(_iPBlokedListService.IPs);
        ItemCount = VMs.Count;
        OnPropertyChanged(nameof(VMs));
        OnPropertyChanged(nameof(ItemCountText));
    }
    private void DisposeItem(IPBlockedViewModel vm)
    {
        VMs.Remove(vm);
        vm.Dispose();
    }
    public override void Update()
    {
        _iPBlokedListService.Update();
    }
    public override void Dispose()
    {
        _iPBlokedListService.IPsChanged -= IPListChanged;
        base.Dispose();
    }
}