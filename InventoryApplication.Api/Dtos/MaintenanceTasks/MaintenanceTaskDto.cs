using InventoryApplication.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace InventoryApplication.Api.Dtos.MaintenanceTasks
{
    public class MaintenanceTaskDto
    {
        [Required(ErrorMessage = "Maintenance task description is required")]
        [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Equipment list is required")]
        [MinLength(1, ErrorMessage = "At least one equipment is required")]
        public virtual IEnumerable<int>? Equipments { get; set; }

        internal MaintenanceTask ParseToMaintenanceTask()
        {
            return new MaintenanceTask
            {
                Description = Description,
                Equipments = Equipments?.Select(e => new Equipment 
                { 
                    Id = e,
                    Brand = string.Empty,
                    Model = string.Empty,
                    PurchaseDate = DateTime.Now,
                }).ToList()
            };
        }
    }
}
