using TaskFlow.Domain.Dto;
using TaskFlow.Domain.Entity;

namespace TaskFlow.Infrastracture.Repository.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskEntity> AddTaskAsync(TaskEntity taskEntity);
        //TaskEntity UpdateTaskAsync(TaskEntity taskEntity);
        void UpdateTask(TaskEntity taskEntity);
        TaskEntity DeleteTaskAsync(TaskEntity taskEntity);
        Task<List<TaskEntity>> GetAllTasksAsync();
        Task<List<TaskDto>> GetTasksWithUserAsync();
        Task<List<TaskDto>> GetTasksByUserAsync(int userId);
        Task<TaskEntity> GetTaskByIdAsync(int taskId);
    }
}