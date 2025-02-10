using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entity;
using TaskFlow.Infrastracture.Connections;
using TaskFlow.Infrastracture.Repository.Interfaces;

namespace TaskFlow.Infrastracture.Repository.Request
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<CategoryEntity> AddCategoryAsync(CategoryEntity categoryEntity)
        {
            if (categoryEntity is null)
                throw new ArgumentNullException(nameof(categoryEntity), "Task cannot be null");

            var result = await _context.CategoryEntity.AddAsync(categoryEntity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public CategoryEntity DeleteCategoryAsync(CategoryEntity categoryEntity)
        {
            var response = _context.CategoryEntity.Remove(categoryEntity);
            return response.Entity;
        }

        public async Task<List<CategoryEntity>> GetAllCategoriesAsync()
        {
            return await _context.CategoryEntity
            .OrderBy(categoryEntity => categoryEntity.Name)
            .Select(categoryEntity => new CategoryEntity
            {
                Id = categoryEntity.Id,
                Name = categoryEntity.Name,
            }).ToListAsync();
        }

        public CategoryEntity UpdateCategoryAsync(CategoryEntity categoryEntity)
        {
            var response = _context.CategoryEntity.Update(categoryEntity);
            return response.Entity;
        }

        public async Task<CategoryEntity?> GetCategoryByIdAsync(int? id)
        {
            return await _context.CategoryEntity.FirstOrDefaultAsync(category => category.Id == id);
        }
    }
}