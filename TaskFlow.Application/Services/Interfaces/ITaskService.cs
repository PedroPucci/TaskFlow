﻿using TaskFlow.Domain.Entity;
using TestWebBackEndDeveloper.Application.ExtensionError;

namespace TaskFlow.Application.Services.Interfaces
{
    public interface ITaskService
    {
        Task<Result<TaskEntity>> AddTaskAsync(TaskEntity taskEntity);
        Task<Result<TaskEntity>> UpdateTaskAsync(TaskEntity taskEntity);
        Task DeleteTaskAsync(int taskId);
        Task<List<TaskEntity>> GetAllTasksAsync();
    }
}
