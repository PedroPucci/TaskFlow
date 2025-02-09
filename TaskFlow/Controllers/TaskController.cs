using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.UnitOfWork;
using TaskFlow.Domain.Dto;
using TaskFlow.Domain.Entity;

namespace TaskFlow.Controllers
{
    public class TaskController : Controller
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public TaskController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskEntity taskEntity)
        {
            if (!ModelState.IsValid)
                return View(taskEntity);

            var result = await _serviceUoW.TaskService.AddTaskAsync(taskEntity);
            if (result.Success)
                return RedirectToAction("Success");

            ModelState.AddModelError("", "Erro ao cadastrar tarefa.");
            return View(taskEntity);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _serviceUoW.TaskService.GetAllTasksAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("ID inválido.");

            await _serviceUoW.TaskService.DeleteTaskAsync(id);

            ModelState.AddModelError("", "Erro ao excluir tarefa.");
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaskEntity taskEntity)
        {
            if (!ModelState.IsValid)
                return View(taskEntity);

            var result = await _serviceUoW.TaskService.UpdateTaskAsync(taskEntity);

            if (result.Success)
                return RedirectToAction("List");

            ModelState.AddModelError("", "Erro ao editar tarefa.");
            return View(taskEntity);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return BadRequest("ID inválido.");

            var result = await _serviceUoW.TaskService.GetTaskByIdAsync(id);

            if (result == null)
                return NotFound();

            return View(result);
        }

        [HttpGet("TasksByUser")]
        public async Task<IActionResult> TasksByUser(int? userId)
        {
            if (userId == null)
            {
                return View(new List<TaskDto>());
            }

            var tasks = await _serviceUoW.TaskService.GetTasksByUserAsync(userId.Value);
            return View(tasks);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
