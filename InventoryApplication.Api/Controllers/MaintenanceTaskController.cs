using InventoryApplication.Api.Dtos;
using InventoryApplication.Api.Dtos.MaintenanceTasks;
using InventoryApplication.Api.Utils;
using InventoryApplication.Services;
using InventoryApplication.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryApplication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceTaskController(MaintenanceTaskService maintenanceTaskService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var maintenanceTasks = await maintenanceTaskService.GetAllMaintanceTasksAsync();
            if (maintenanceTasks == null || !maintenanceTasks.Any())
            {
                return NotFound(InvalidResult.Create(Messages.MaintenanceTaskNotFound));
            }

            return Ok(maintenanceTasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest(InvalidResult.Create(Messages.MaintenanceTaskNotFound));
            }
            var maintenanceTask = await maintenanceTaskService.GetMaintanceTaskByIdAsync(id);
            if (maintenanceTask == null)
            {
                return NotFound(InvalidResult.Create(Messages.MaintenanceTaskNotFound));
            }

            return Ok(maintenanceTask);
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MaintenanceTaskDto maintenanceTaskDto)
        {
            var maintenanceTask = maintenanceTaskDto.ParseToMaintenanceTask();
            await maintenanceTaskService.AddMaintanceTaskAsync(maintenanceTask);
            return StatusCode(StatusCodes.Status201Created, Messages.MaintenanceTaskCreated);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MaintenanceTaskDto maintenanceTaskDto)
        {
            if (id <= 0)
            {
                return BadRequest(InvalidResult.Create(Messages.MaintenanceTaskNotFound));
            }

            var maintenanceTask = maintenanceTaskDto.ParseToMaintenanceTask();
            maintenanceTask.Id = id;

            await maintenanceTaskService.UpdateMaintanceTaskAsync(maintenanceTask);
            return StatusCode(StatusCodes.Status200OK, Messages.MaintenanceTaskUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(InvalidResult.Create(Messages.MaintenanceTaskNotFound));
            }

            await maintenanceTaskService.DeleteMaintanceTaskAsync(id);

            return StatusCode(StatusCodes.Status200OK, Messages.MaintenanceTaskDeleted);
        }
    }
}
