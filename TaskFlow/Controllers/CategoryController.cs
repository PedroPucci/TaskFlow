using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.UnitOfWork;
using TaskFlow.Domain.Entity;

namespace TaskFlow.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public CategoryController(IUnitOfWorkService unitOfWorkService)
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
        public async Task<IActionResult> Create(CategoryEntity categoryEntity)
        {
            if (!ModelState.IsValid)
                return View(categoryEntity);

            var result = await _serviceUoW.CategoryService.AddCategoryAsync(categoryEntity);
            if (result.Success)
                return RedirectToAction("Success");

            ModelState.AddModelError("", "Erro ao cadastrar a categoria.");
            return View(categoryEntity);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var categories = await _serviceUoW.CategoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("ID inválido.");

            await _serviceUoW.CategoryService.DeleteCategoryAsync(id);

            ModelState.AddModelError("", "Erro ao excluir a categoria.");
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryEntity categoryEntity)
        {
            if (!ModelState.IsValid)
                return View(categoryEntity);

            var result = await _serviceUoW.CategoryService.UpdateCategoryAsync(categoryEntity);

            if (result.Success)
                return RedirectToAction("List");

            ModelState.AddModelError("", "Erro ao editar a categoria.");
            return View(categoryEntity);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return BadRequest("ID inválido.");

            var user = await _serviceUoW.CategoryService.GetCategoryByIdAsync(id);

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
