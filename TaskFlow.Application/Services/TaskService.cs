using FluentValidation;
using Serilog;
using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Domain.Dto;
using TaskFlow.Domain.Entity;
using TaskFlow.Domain.Enums;
using TaskFlow.Infrastracture.Repository.RepositoryUoW;
using TaskFlow.Shared.Logging;
using TaskFlow.Shared.Validator;
using TestWebBackEndDeveloper.Application.ExtensionError;

namespace TaskFlow.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepositoryUoW _repositoryUoW;

        public TaskService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public async Task<Result<TaskEntity>> AddTaskAsync(TaskEntity taskEntity)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var isValidTask = await IsValidTaskRequest(taskEntity);

                if (!isValidTask.Success)
                {
                    Log.Error(LogMessages.InvalidTaskInputs());
                    return Result<TaskEntity>.Error(isValidTask.Message);
                }

                taskEntity.ModificationDate = DateTime.UtcNow;
                taskEntity.Title = taskEntity.Description?.Trim().ToLower();
                taskEntity.Status = TaskEntityStatus.Pending;
                var result = await _repositoryUoW.TaskRepository.AddTaskAsync(taskEntity);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();

                return Result<TaskEntity>.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.AddingUserError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to add a new Task.");
            }
            finally
            {
                Log.Error(LogMessages.AddingUserSuccess());
                transaction.Dispose();
            }
        }

        public Task DeleteTaskAsync(int taskId)
        {
            throw new NotImplementedException();
        }
        public async Task<List<TaskEntity>> GetAllTasksAsync()
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                List<TaskEntity> tasks = await _repositoryUoW.TaskRepository.GetAllTasksAsync();
                _repositoryUoW.Commit();
                return tasks;
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.GetAllTasksError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to loading the list Task.");
            }
            finally
            {
                Log.Error(LogMessages.GetAllTasksSuccess());
                transaction.Dispose();
            }
        }

        public async Task<List<TaskDto>> GetTasksWithUserAsync()
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                List<TaskDto> tasks = await _repositoryUoW.TaskRepository.GetTasksWithUserAsync();
                _repositoryUoW.Commit();
                return tasks;
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.GetAllTasksError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to loading the list Task with info user.");
            }
            finally
            {
                Log.Error(LogMessages.GetAllTasksSuccess());
                transaction.Dispose();
            }
        }

        public async Task<List<TaskDto>> GetTasksByUserAsync(int userId)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                List<TaskDto> tasks = await _repositoryUoW.TaskRepository.GetTasksByUserAsync(userId);
                _repositoryUoW.Commit();
                return tasks;
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.GetAllTasksError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to loading the list Task bu userId.");
            }
            finally
            {
                Log.Error(LogMessages.GetAllTasksSuccess());
                transaction.Dispose();
            }
        }

        public async Task<Result<TaskEntity>> UpdateTaskAsync(TaskEntity taskEntity)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var taskById = await _repositoryUoW.TaskRepository.GetTaskByIdAsync(taskEntity.Id);
                if (taskById == null)
                    throw new InvalidOperationException("Message: Error updating Task");

                taskById.Title = taskEntity.Title;
                taskById.Description = taskEntity.Description;
                taskById.DueDate = taskEntity.DueDate;
                taskById.ModificationDate = DateTime.UtcNow;
                taskById.UserId = taskEntity.UserId;

                _repositoryUoW.TaskRepository.UpdateTask(taskById);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();

                return Result<TaskEntity>.Ok();
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

        private async Task<Result<TaskEntity>> IsValidTaskRequest(TaskEntity taskEntity)
        {
            var requestValidator = await new TaskRequestValidator().ValidateAsync(taskEntity);
            if (!requestValidator.IsValid)
            {
                string errorMessage = string.Join(" ", requestValidator.Errors.Select(e => e.ErrorMessage));
                errorMessage = errorMessage.Replace(Environment.NewLine, "");
                return Result<TaskEntity>.Error(errorMessage);
            }

            return Result<TaskEntity>.Ok();
        }
    }
}