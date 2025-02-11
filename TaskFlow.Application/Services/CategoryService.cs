using Serilog;
using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Domain.Entity;
using TaskFlow.Infrastracture.Repository.RepositoryUoW;
using TaskFlow.Shared.Logging;
using TaskFlow.Shared.Validator;
using TestWebBackEndDeveloper.Application.ExtensionError;

namespace TaskFlow.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryUoW _repositoryUoW;

        public CategoryService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public async Task<Result<CategoryEntity>> AddCategoryAsync(CategoryEntity categoryEntity)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var isValidCategory = await IsValidCategoryRequest(categoryEntity);

                if (!isValidCategory.Success)
                {
                    Log.Error(LogMessages.InvalidCategoryInputs());
                    return Result<CategoryEntity>.Error(isValidCategory.Message);
                }

                categoryEntity.ModificationDate = DateTime.UtcNow;
                categoryEntity.Name = categoryEntity.Name;
                var result = await _repositoryUoW.CategoryRepository.AddCategoryAsync(categoryEntity);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();

                return Result<CategoryEntity>.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.AddingCategoryError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to add a new Category.");
            }
            finally
            {
                Log.Error(LogMessages.AddingCategorySuccess());
                transaction.Dispose();
            }
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var categoryToDelete = await _repositoryUoW.CategoryRepository.GetCategoryByIdAsync(categoryId);
                if (categoryToDelete is not null)
                    _repositoryUoW.CategoryRepository.DeleteCategoryAsync(categoryToDelete);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.DeleteCategoryError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to delete a Category.");
            }
            finally
            {
                Log.Error(LogMessages.DeleteCategorySuccess());
                transaction.Dispose();
            }
        }

        public async Task<List<CategoryEntity>> GetAllCategoriesAsync()
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                List<CategoryEntity> categories = await _repositoryUoW.CategoryRepository.GetAllCategoriesAsync();
                _repositoryUoW.Commit();
                return categories;
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.GetAllCategoriesError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to loading the list Category.");
            }
            finally
            {
                Log.Error(LogMessages.GetAllCategoriesSuccess());
                transaction.Dispose();
            }
        }

        public async Task<Result<CategoryEntity>> UpdateCategoryAsync(CategoryEntity categoryEntity)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var categoryById = await _repositoryUoW.CategoryRepository.GetCategoryByIdAsync(categoryEntity.Id);
                if (categoryById == null)
                    throw new InvalidOperationException("Message: Error updating User");

                categoryById.Name = categoryEntity.Name;
                categoryById.ModificationDate = DateTime.UtcNow;

                _repositoryUoW.CategoryRepository.UpdateCategoryAsync(categoryById);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();

                return Result<CategoryEntity>.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.UpdatingErrorCategory(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error updating Category", ex);
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public async Task<CategoryEntity?> GetCategoryByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            return await _repositoryUoW.CategoryRepository.GetCategoryByIdAsync(id);
        }

        private async Task<Result<CategoryEntity>> IsValidCategoryRequest(CategoryEntity categoryEntity)
        {
            var requestValidator = await new CategoryRequestValidator().ValidateAsync(categoryEntity);
            if (!requestValidator.IsValid)
            {
                string errorMessage = string.Join(" ", requestValidator.Errors.Select(e => e.ErrorMessage));
                errorMessage = errorMessage.Replace(Environment.NewLine, "");
                return Result<CategoryEntity>.Error(errorMessage);
            }

            return Result<CategoryEntity>.Ok();
        }
    }
}