using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.UnitOfWork;
using TaskFlow.Domain.Entity;

namespace TaskFlow.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public UserController(IUnitOfWorkService unitOfWorkService)
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
        public async Task<IActionResult> Create(UserEntity userEntity)
        {
            if (!ModelState.IsValid)
                return View(userEntity);

            var result = await _serviceUoW.UserService.AddUserAsync(userEntity);
            if (result.Success)
                return RedirectToAction("Success");

            ModelState.AddModelError("", "Erro ao cadastrar usuário.");
            return View(userEntity);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _serviceUoW.UserService.GetAllUsersAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("ID inválido.");

            await _serviceUoW.UserService.DeleteUserAsync(id);

            ModelState.AddModelError("", "Erro ao excluir usuário.");
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEntity userEntity)
        {
            if (!ModelState.IsValid)
                return View(userEntity);

            var result = await _serviceUoW.UserService.UpdateUserAsync(userEntity);

            if (result.Success)
                return RedirectToAction("List");

            ModelState.AddModelError("", "Erro ao editar usuário.");
            return View(userEntity);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return BadRequest("ID inválido.");

            var user = await _serviceUoW.UserService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
