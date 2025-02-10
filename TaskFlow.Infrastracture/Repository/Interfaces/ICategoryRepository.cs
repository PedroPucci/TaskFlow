using TaskFlow.Domain.Entity;

namespace TaskFlow.Infrastracture.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<CategoryEntity> AddCategoryAsync(CategoryEntity categoryEntity);
        CategoryEntity UpdateCategoryAsync(CategoryEntity categoryEntity);
        CategoryEntity DeleteCategoryAsync(CategoryEntity categoryEntity);
        Task<List<CategoryEntity>> GetAllCategoriesAsync();
        Task<CategoryEntity?> GetCategoryByIdAsync(int? id);
    }
}