using TaskFlow.Domain.Entity;

namespace TaskFlow.Infrastracture.Repository.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskEntity> AddTaskAsync(TaskEntity taskEntity);
        TaskEntity UpdateTaskAsync(TaskEntity taskEntity);
        TaskEntity DeleteTaskAsync(TaskEntity taskEntity);
        Task<List<TaskEntity>> GetAllTasksAsync();
    }
}