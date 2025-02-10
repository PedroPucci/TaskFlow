using TaskFlow.Domain.Entity;
using TestWebBackEndDeveloper.Application.ExtensionError;

namespace TaskFlow.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Result<CategoryEntity>> AddCategoryAsync(CategoryEntity categoryEntity);
        Task<Result<CategoryEntity>> UpdateCategoryAsync(CategoryEntity categoryEntity);
        Task DeleteCategoryAsync(int categoryId);
        Task<List<CategoryEntity>> GetAllCategoriesAsync();
    }
}