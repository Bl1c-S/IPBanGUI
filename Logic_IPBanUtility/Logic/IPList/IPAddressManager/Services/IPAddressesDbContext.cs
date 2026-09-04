﻿using Microsoft.EntityFrameworkCore;

namespace Logic_IPBanUtility.Logic.IPList;

public class IPAddressesDbContext : DbContext
{
     private readonly string _path;
     private readonly bool _createIfMissing;

     public IPAddressesDbContext(string path, bool createIfMissing = false)
     {
          _path = path;
          _createIfMissing = createIfMissing;
     }

     public DbSet<IPAddressEntityDTO> IPAddresses { get; set; }

     public void Add(IPAddressEntity entity)
     {
          var dto = IPAddressEntityDTO.ToDTO(entity);
          IPAddresses.Add(dto);
          SaveChanges();
     }
     public void Remove(string iPAddressText)
     {
          LoadTable();
          var targetDTO = IPAddresses.FirstOrDefault(x => x.IPAddressText == iPAddressText);
          if (targetDTO != null)
          {
               IPAddresses.Remove(targetDTO);
               SaveChanges();
          }
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
          if (_createIfMissing)
          {
               var directory = Path.GetDirectoryName(Path.GetFullPath(_path));
               if (!string.IsNullOrEmpty(directory))
                    Directory.CreateDirectory(directory);
          }

          var mode = _createIfMissing ? "ReadWriteCreate" : "ReadWrite";
          optionsBuilder.UseSqlite($"Data Source={_path};Cache=Shared;Mode={mode}");
     }
}
