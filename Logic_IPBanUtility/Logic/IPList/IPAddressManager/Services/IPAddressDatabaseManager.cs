using Logic_IPBanUtility.Setting;

namespace Logic_IPBanUtility.Logic.IPList;

public class IPAddressDatabaseManager
{
    private string _path;
    public IPAddressDatabaseManager(Settings settings)
    {
        _path = settings.IPBan.Sqlite_db;
    }
    public void Add(IPAddressEntity entity)
    {
        using (var dbContext = new IPAddressesDbContext(_path))
        {
            try
            {
                dbContext.Add(entity);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка додавання {entity.IPAddressText} до бази даних {_path}. \n\r {ex.Message}");
            }
        }
    }
    public List<IPAddressEntity> GetAll()
    {
        using (var dbContext = new IPAddressesDbContext(_path))
        {
            try
            {
                var ips = dbContext.GetAll();
                return ips;
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка підключення до бази даних {_path}. \n\r {ex.Message}");
            }
        }
    }
    public void Remove(IPAddressEntity entity)
    {
        using (var dbContext = new IPAddressesDbContext(_path))
        {
            try
            {
                dbContext.Remove(entity.IPAddressText);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка видалення {entity.IPAddressText} з бази {_path}. \n\r {ex.Message}");
            }
        }
    }
    public void RemoveAll()
    {
        using (var dbContext = new IPAddressesDbContext(_path))
        {
            try
            {
                dbContext.RemoveAll();
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Помилка видалення всіх заблокованих ІР з бази {_path}. \n\r {ex.Message}");
            }
        }
    }
}