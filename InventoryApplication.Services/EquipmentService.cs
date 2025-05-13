using InventoryApplication.Domain.Models;
using InventoryApplication.Domain.Repository;
using InventoryApplication.Domain.Repository.Base;
using InventoryApplication.Infrastructure.Repository.Context;
using Microsoft.Extensions.Logging;

namespace InventoryApplication.Services
{
    public class EquipmentService(IEquipmentRepository equipmentRepository, InventoryContext inventoryContext)
    {

        public async Task<List<Equipment>> GetAllEquipmentsAsync() => await equipmentRepository.GetAllAsync();
        public async Task<Paged<Equipment>> GetAllEquipmentsAsync(int pageNumber, int pageSize) => await equipmentRepository.GetAllAsync(pageNumber, pageSize);

        public async Task<Equipment?> GetEquipmentByIdAsync(int id) => await equipmentRepository.GetByIdAsync(id);

        public async Task AddEquipmentAsync(Equipment equipment)
        {
            await equipmentRepository.AddAsync(equipment);
            await inventoryContext.SaveChangesAsync();
        }

        public async Task UpdateEquipmentAsync(Equipment equipment)
        {
            await equipmentRepository.UpdateAsync(equipment);
            await inventoryContext.SaveChangesAsync();
        }

        public async Task DeleteEquipmentAsync(int id)
        {
            await equipmentRepository.DeleteAsync(id);
            await inventoryContext.SaveChangesAsync();
        }   
    }
}
