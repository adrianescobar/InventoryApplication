using InventoryApplication.Domain.Models;
using InventoryApplication.Domain.Repository;
using InventoryApplication.Infrastructure.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace InventoryApplication.Infrastructure.Repository
{
    public class EquipmentTypeRepository : IEquipmentTypeRepository
    {
        private readonly InventoryContext _context;

        public EquipmentTypeRepository(InventoryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EquipmentType>> GetAllAsync() => await _context.EquipmentTypes.ToListAsync();
    }
}
