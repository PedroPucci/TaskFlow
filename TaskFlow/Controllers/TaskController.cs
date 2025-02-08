using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.UnitOfWork;
using TaskFlow.Domain.Entity;

namespace TaskFlow.Controllers
{
    [ApiController]
    [Route("api/v1/task")]
    public class TaskController : Controller
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public TaskController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddTask([FromBody] TaskEntity taskEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.TaskService.AddTaskAsync(taskEntity);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateTask([FromBody] TaskEntity taskEntity)
        {
            var result = await _serviceUoW.TaskService.UpdateTaskAsync(taskEntity);
            return result.Success ? Ok(result) : BadRequest(taskEntity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _serviceUoW.TaskService.DeleteTaskAsync(id);
            return Ok();
        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserEntity>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllTaks()
        {
            var tasks = await _serviceUoW.TaskService.GetAllTasksAsync();
            return Ok(tasks);
        }
    }
}
