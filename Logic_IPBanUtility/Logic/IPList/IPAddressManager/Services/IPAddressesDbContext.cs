using Microsoft.EntityFrameworkCore;

namespace Logic_IPBanUtility.Logic.IPList;

public class IPAddressesDbContext : DbContext
{
    private string _path;
    public IPAddressesDbContext(string path) => _path = path;

    public DbSet<IPAddressEntityDTO> IPAddresses { get; set; }

    public void Add(IPAddressEntity entity)
    {
        var dto = IPAddressEntityDTO.ToDTO(entity);
        IPAddresses.Add(dto);
    }
    public void Remove(string iPAddressText)
    {
        var targetDTO = IPAddresses.FirstOrDefault(x => x.IPAddressText == iPAddressText);
        if (targetDTO != null)
            IPAddresses.Remove(targetDTO);
    }
    public void RemoveAll()
    {
        foreach (var ip in IPAddresses)
            Remove(ip);
    }
    public void LoadTable() => IPAddresses.Load();
    public List<IPAddressEntity> GetAll()
    {
        var dto = IPAddresses.ToList();
        return Convert(dto);
    }

    private List<IPAddressEntity> Convert(List<IPAddressEntityDTO> addressesDTO) => addressesDTO.Select(dto => dto.ToEntity()).ToList();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_path}");
    }
}