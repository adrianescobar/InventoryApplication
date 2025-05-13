using InventoryApplication.Domain.Models;
using InventoryApplication.Domain.Repository;
using InventoryApplication.Infrastructure.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace InventoryApplication.Infrastructure.Repository
{
    public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(InventoryContext context) : base(context)
        {
        }

        public override Task<List<Equipment>> GetAllAsync()
        {
            return _dbSet.AsNoTracking()
                .Include(e => e.EquipmentType)
                .OrderDescending()
                .ToListAsync();
        }
        public async Task<IEnumerable<Equipment>> GetByIds(int[] ids)
        {
            return await _dbSet
                .Where(e => ids.Contains(e.Id))
                .OrderDescending()
                .ToListAsync();
        }
    }
}
