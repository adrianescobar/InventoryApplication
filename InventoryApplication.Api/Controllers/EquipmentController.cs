using InventoryApplication.Api.Dtos;
using InventoryApplication.Api.Dtos.Equipments;
using InventoryApplication.Api.Utils;
using InventoryApplication.Infrastructure.Repository;
using InventoryApplication.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryApplication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController(EquipmentService equipmentService, ILogger<EquipmentController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var equipments = await equipmentService.GetAllEquipmentsAsync();

            if (equipments == null || !equipments.Any())
            {
                return NotFound(InvalidResult.Create(Messages.EquipmentNotFound));
            }
            return Ok(equipments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest(InvalidResult.Create(Messages.EquipmentNotFound));
            }

            var equipment = await equipmentService.GetEquipmentByIdAsync(id);

            if (equipment == null)
            {
                return NotFound(Messages.EquipmentNotFound);
            }

            return Ok(equipment);
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EquipmentDto equipmentDto)
        {
            var equipment = equipmentDto.ParseToEquipment();
            await equipmentService.AddEquipmentAsync(equipment);
            return StatusCode(StatusCodes.Status201Created, Messages.EquipmentCreated);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EquipmentDto equipmentDto)
        {
            if (id <= 0)
            {
                return BadRequest(InvalidResult.Create(Messages.EquipmentNotFound));
            }

            var equipment = equipmentDto.ParseToEquipment();
            equipment.Id = id;

            await equipmentService.UpdateEquipmentAsync(equipment);
            return StatusCode(StatusCodes.Status200OK, Messages.EquipmentUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(InvalidResult.Create(Messages.EquipmentNotFound));
            }

            await equipmentService.DeleteEquipmentAsync(id);
            return StatusCode(StatusCodes.Status200OK, Messages.EquipmentDeleted);
        }
    }
}
