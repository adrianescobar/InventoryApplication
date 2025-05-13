using InventoryApplication.Domain.Common;

namespace InventoryApplication.Domain.Models
{
    public class Equipment : BaseModel
    {
        /// <summary>
        /// Equipment brand
        /// </summary>
        public required string Brand { get; set; }
        /// <summary>
        /// Equipment model
        /// </summary>
        public required string Model { get; set; }
        /// <summary>
        /// Purchase date
        /// </summary>
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        /// <summary>
        /// Equipment serial number
        /// </summary>
        public string? SerialNumber { get; set; }
        /// <summary>
        /// Equipment type identifier
        /// </summary>
        public int EquipmentTypeId { get; set; }
        /// <summary>
        /// Equipment type property navigation
        /// </summary>
        public virtual EquipmentType? EquipmentType { get; set; }
        /// <summary>
        /// Maintenance tasks property navigation
        /// </summary>
        public virtual ICollection<MaintenanceTask>? MaintenanceTasks { get; set; }
    }
}
