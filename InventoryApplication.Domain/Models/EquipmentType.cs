using InventoryApplication.Domain.Common;

namespace InventoryApplication.Domain.Models
{
    public class EquipmentType : BaseModel
    {
        /// <summary>
        /// Equipment type description
        /// </summary>
        public required string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Equipment>? Equipments { get; set; }
    }
}
