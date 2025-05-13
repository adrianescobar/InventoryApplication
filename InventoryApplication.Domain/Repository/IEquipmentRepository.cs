using InventoryApplication.Domain.Models;
using InventoryApplication.Domain.Repository.Base;

namespace InventoryApplication.Domain.Repository
{
    public interface IEquipmentRepository : IRepository<Equipment>
    {
      Task<IEnumerable<Equipment>> GetByIds(int[] ids);
    }
}
