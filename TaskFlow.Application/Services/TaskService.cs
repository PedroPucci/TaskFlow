﻿using Serilog;
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
                var isValidUser = await IsValidTaskRequest(taskEntity);

                if (!isValidUser.Success)
                {
                    Log.Error(LogMessages.InvalidTaskInputs());
                    return Result<TaskEntity>.Error(isValidUser.Message);
                }

                taskEntity.ModificationDate = DateTime.UtcNow;
                taskEntity.DueDate = DateTime.SpecifyKind(taskEntity.DueDate, DateTimeKind.Utc);
                var result = await _repositoryUoW.TaskRepository.AddTaskAsync(taskEntity);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();

                return Result<TaskEntity>.Ok();
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

        public async Task DeleteTaskAsync(int taskId)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                var taskToDelete = await _repositoryUoW.TaskRepository.GetTaskByIdAsync(taskId);                

                if (taskToDelete is not null)
                    _repositoryUoW.TaskRepository.DeleteTaskAsync(taskToDelete);

                await _repositoryUoW.SaveAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.DeleteTaskError(ex));
                transaction.Rollback();
                throw new InvalidOperationException("Message: Error to delete a Task.");
            }
            finally
            {
                Log.Error(LogMessages.DeleteTaskSuccess());
                transaction.Dispose();
            }
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
                taskById.ModificationDate = DateTime.UtcNow;
                taskById.Status = taskEntity.Status;
                taskById.DueDate = DateTime.SpecifyKind(taskEntity.DueDate, DateTimeKind.Utc);

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

        public async Task<TaskEntity> GetTaskByIdAsync(int taskId)
        {
            using var transaction = _repositoryUoW.BeginTransaction();
            try
            {
                if (taskId <= 0)
                {
                    Log.Error("Task ID inválido: " + taskId);
                    throw new ArgumentException("O ID da tarefa deve ser um número positivo.");
                }

                var task = await _repositoryUoW.TaskRepository.GetTaskByIdAsync(taskId);

                if (task == null)
                {
                    Log.Warning($"Nenhuma tarefa encontrada com o ID: {taskId}");
                    throw new KeyNotFoundException($"Nenhuma tarefa encontrada com o ID: {taskId}");
                }

                _repositoryUoW.Commit();
                return task;
            }
            catch (Exception ex)
            {
                Log.Error($"Erro ao buscar a tarefa com ID {taskId}: {ex.Message}");
                transaction.Rollback();
                throw new InvalidOperationException("Erro ao recuperar a tarefa.", ex);
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