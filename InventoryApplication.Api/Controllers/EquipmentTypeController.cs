using InventoryApplication.Api.Dtos;
using InventoryApplication.Api.Utils;
using InventoryApplication.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApplication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentTypeController(IEquipmentTypeRepository equipmentTypeRepository) : ControllerBase
    {
        [HttpGet(Name = "GetEquipmentTypes")]
        public async Task<IActionResult> Get()
        {
            var equipmentTypes = await equipmentTypeRepository.GetAllAsync();
            if (equipmentTypes == null || !equipmentTypes.Any())
            {
                return NotFound(InvalidResult.Create(Messages.EquipmentTypeNotFound));
            }
            return Ok(equipmentTypes);
        }
    }
}
