using InventoryApplication.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace InventoryApplication.Api.Dtos.Equipments
{
    public class EquipmentDto
    {
        [Required(ErrorMessage = "Equipment brand is required")]
        [StringLength(100, ErrorMessage = "Brand name cannot exceed 100 characters")]
        public required string Brand { get; set; }
        [Required(ErrorMessage = "Equipment model is required")]
        [StringLength(100, ErrorMessage = "Model name cannot exceed 100 characters")]
        public required string Model { get; set; }
        [Required(ErrorMessage = "Purchase date is required")]
        [DataType(DataType.DateTime)]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        [StringLength(100, ErrorMessage = "Serial number cannot exceed 100 characters")]
        public string? SerialNumber { get; set; }
        [Required(ErrorMessage = "Equipment type is required")]
        public int EquipmentTypeId { get; set; }

        public Equipment ParseToEquipment()
        {
            return new Equipment
            {
                Brand = Brand,
                Model = Model,
                PurchaseDate = PurchaseDate,
                SerialNumber = SerialNumber,
                EquipmentTypeId = EquipmentTypeId
            };
        }
    }
}
