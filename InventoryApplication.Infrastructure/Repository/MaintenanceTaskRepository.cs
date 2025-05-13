using InventoryApplication.Domain.Models;
using InventoryApplication.Domain.Repository;
using InventoryApplication.Domain.Repository.Base;
using InventoryApplication.Infrastructure.Repository.Context;
using InventoryApplication.Infrastructure.Repository.Utils;
using Microsoft.EntityFrameworkCore;

namespace InventoryApplication.Infrastructure.Repository
{
    public class MaintenanceTaskRepository : Repository<MaintenanceTask>, IMaintenanceTaskRepository
    {
        public MaintenanceTaskRepository(InventoryContext context) : base(context)
        {

        }

        public override Task<MaintenanceTask?> GetByIdAsync(int id) => 
            _dbSet.Include(e => e.Equipments)
                .FirstOrDefaultAsync(e => e.Id == id);

        public override Task<List<MaintenanceTask>> GetAllAsync()
        {
            return _dbSet.AsNoTracking()
                .Include(e => e.Equipments)
                .ThenInclude(e => e.EquipmentType)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public override Task<Paged<MaintenanceTask>> GetAllAsync(int pageNumber, int pageSize)
        {
            return _dbSet
                .AsNoTracking()
                .Include(e => e.Equipments)
                .OrderByDescending(x => x.Id)
                .ToPagedAsync(pageNumber, pageSize);
        }
    }
}
