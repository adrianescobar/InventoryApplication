using InventoryApplication.Domain.Models;
using InventoryApplication.Domain.Repository;
using InventoryApplication.Domain.Repository.Base;
using InventoryApplication.Infrastructure.Repository.Context;
using InventoryApplication.Services.Exceptions;
using InventoryApplication.Services.Utils;
using Microsoft.Extensions.Logging;

namespace InventoryApplication.Services
{
    public class MaintenanceTaskService(IMaintenanceTaskRepository maintenanceTaskRepository, IEquipmentRepository equipmentRepository, InventoryContext inventoryContext)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MaintenanceTask>> GetAllMaintanceTasksAsync() => await maintenanceTaskRepository.GetAllAsync();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<Paged<MaintenanceTask>> GetAllMaintanceTasksAsync(int pageNumber, int pageSize) => await maintenanceTaskRepository.GetAllAsync(pageNumber, pageSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MaintenanceTask?> GetMaintanceTaskByIdAsync(int id) => await maintenanceTaskRepository.GetByIdAsync(id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maintenanceTask"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InventoryApplicationException"></exception>
        public async Task AddMaintanceTaskAsync(MaintenanceTask maintenanceTask)
        {
            if (maintenanceTask == null)
            {
                throw new ArgumentNullException(nameof(maintenanceTask), "Maintenance task cannot be null.");
            }

            var equipmentIds = maintenanceTask?.Equipments?.Select(e => e.Id).ToArray();

            if (equipmentIds == null || equipmentIds.Length == 0)
            {
                throw new InventoryApplicationException(Messages.InvalidEquipmentIds);
            }

            var equipments = await equipmentRepository.GetByIds(equipmentIds);

            if (equipments == null || !equipments.Any() || equipments.Count() != maintenanceTask?.Equipments?.Count)
            {
                throw new InventoryApplicationException(Messages.InvalidEquipmentIds);
            }

            maintenanceTask.Equipments = equipments.ToList();

            await maintenanceTaskRepository.AddAsync(maintenanceTask);

            await inventoryContext.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maintenanceTask"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InventoryApplicationException"></exception>
        public async Task UpdateMaintanceTaskAsync(MaintenanceTask maintenanceTask)
        {
            if (maintenanceTask == null)
            {
                throw new ArgumentNullException(nameof(maintenanceTask), "Maintenance task cannot be null.");
            }

            var equipmentIds = maintenanceTask?.Equipments?.Select(e => e.Id).ToArray();

            if (equipmentIds == null || equipmentIds.Length == 0)
            {
                throw new InventoryApplicationException(Messages.InvalidEquipmentIds);
            }

            var existingMaintenanceTask = await maintenanceTaskRepository.GetByIdAsync(maintenanceTask.Id);

            if (existingMaintenanceTask == null)
            {
                throw new InventoryApplicationException($"No maintenance task found with ID {maintenanceTask.Id}.");
            }

            var equipments = await equipmentRepository.GetByIds(equipmentIds);

            if (equipments == null || !equipments.Any() || equipmentIds.Length != equipments.Count())
            {
                throw new InventoryApplicationException(Messages.InvalidEquipmentIds);
            }

            existingMaintenanceTask.Description = maintenanceTask.Description;

            var equipmentsToRemove = existingMaintenanceTask.Equipments?
                .Where(x => !equipmentIds.Any(y => y == x.Id))
                .ToList();

            foreach (var equipment in equipmentsToRemove)
            {
                existingMaintenanceTask.Equipments?.Remove(equipment);
            }

            var equipmentsToAdd = equipments
                .Where(x => !existingMaintenanceTask.Equipments.Any(y => y?.Id == x.Id))
                .ToList();

            foreach (var equipment in equipmentsToAdd)
            {
                existingMaintenanceTask.Equipments?.Add(equipment);
            }

            await inventoryContext.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteMaintanceTaskAsync(int id)
        {
            await maintenanceTaskRepository.DeleteAsync(id);
            await inventoryContext.SaveChangesAsync();
        }
    }
}
