using InventoryApplication.Domain.Models;

namespace InventoryApplication.Domain.Repository
{
    public interface IEquipmentTypeRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EquipmentType>> GetAllAsync();
    }
}
