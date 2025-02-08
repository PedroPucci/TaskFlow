using Serilog;
using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Domain.Entity;
using TaskFlow.Domain.General;
using TaskFlow.Infrastracture.Repository.RepositoryUoW;
using TaskFlow.Shared.Logging;
using TaskFlow.Shared.Validator;
using TestWebBackEndDeveloper.Application.ExtensionError;

namespace TaskFlow.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryUoW _repositoryUoW;

        public UserService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public async Task<Result<UserEntity>> AddUserAsync(UserEntity userEntity)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var isValidUser = await IsValidUserRequest(userEntity);

                if (!isValidUser.Success)
                {
                    Log.Error(LogMessages.InvalidUserInputs());
                    return Result<UserEntity>.Error(isValidUser.Message);
                }

                userEntity.ModificationDate = DateTime.UtcNow;
                userEntity.Email = userEntity.Email?.Trim().ToLower();
                var result = await _repositoryUoW.UserRepository.AddUserAsync(userEntity);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();

                return Result<UserEntity>.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.AddingUserError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to add a new User.");
            }
            finally
            {
                Log.Error(LogMessages.AddingUserSuccess());
                transaction.Dispose();
            }
        }

        public async Task<Result<UserEntity>> UpdateUserAsync(UserEntity userEntity)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var userById = await _repositoryUoW.UserRepository.GetUserByIdAsync(userEntity.Id);
                if (userById == null)
                    throw new InvalidOperationException("Message: Error updating User");

                userById.Name = userEntity.Name;
                userById.Email = userEntity.Email;
                userById.ModificationDate = DateTime.UtcNow;
                userById.IsActive = userEntity.IsActive;

                _repositoryUoW.UserRepository.UpdateUserAsync(userById);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();

                return Result<UserEntity>.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.UpdatingErrorTask(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error updating Task", ex);
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var userToDelete = await _repositoryUoW.UserRepository.GetUserByIdAsync(userId);

                ValidateUserExistsForAction(userToDelete, "delete");

                if (userToDelete is not null)
                    _repositoryUoW.UserRepository.DeleteUserAsync(userToDelete);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.DeleteUserError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to delete a AccountUser.");
            }
            finally
            {
                Log.Error(LogMessages.DeleteUserSuccess());
                transaction.Dispose();
            }
        }

        public async Task<List<UserEntity>> GetAllUsersAsync()
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                List<UserEntity> users = await _repositoryUoW.UserRepository.GetAllUsersAsync();
                _repositoryUoW.Commit();
                return users;
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.GetAllUsersError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to loading the list User.");
            }
            finally
            {
                Log.Error(LogMessages.GetAllUsersSuccess());
                transaction.Dispose();
            }
        }

        private async Task<Result<UserEntity>> IsValidUserRequest(UserEntity userEntity)
        {
            var requestValidator = await new UserRequestValidator().ValidateAsync(userEntity);
            if (!requestValidator.IsValid)
            {
                string errorMessage = string.Join(" ", requestValidator.Errors.Select(e => e.ErrorMessage));
                errorMessage = errorMessage.Replace(Environment.NewLine, "");
                return Result<UserEntity>.Error(errorMessage);
            }

            return Result<UserEntity>.Ok();
        }

        private void ValidateUserExistsForAction(UserEntity? user, string action)
        {
            if (user is null)
            {
                Log.Error($"Message: Error to {action} User");
                throw new InvalidOperationException($"User does not found for {action} action!");
            }
        }

        private Result<UserEntity> ValidateUser(UserEntity userEntity, Result<UserEntity> isValidUser)
        {
            if (!isValidUser.Success)
            {
                Log.Error(LogMessages.InvalidUserInputs());
                return Result<UserEntity>.Error(isValidUser.Message);
            }

            if (string.IsNullOrWhiteSpace(userEntity.Name))
            {
                Log.Error(LogMessages.NullOrEmptyUserName());
                return Result<UserEntity>.Error("Message: The Name field cannot be null, empty, or whitespace.");
            }

            return Result<UserEntity>.Ok();
        }
    }
}
