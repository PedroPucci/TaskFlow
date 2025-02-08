using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Domain.Entity;
using TaskFlow.Infrastracture.Repository.RepositoryUoW;
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

        public Task<Result<TaskEntity>> AddTaskAsync(TaskEntity taskEntity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTaskAsync(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskEntity>> GetAllTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<TaskEntity>> UpdateTaskAsync(TaskEntity taskEntity)
        {
            throw new NotImplementedException();
        }
    }
}