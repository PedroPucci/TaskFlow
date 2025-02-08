using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entity;
using TaskFlow.Infrastracture.Connections;
using TaskFlow.Infrastracture.Repository.Interfaces;

namespace TaskFlow.Infrastracture.Repository.Request
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _context;

        public TaskRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<TaskEntity> AddTaskAsync(TaskEntity taskEntity)
        {
            if (taskEntity is null)
                throw new ArgumentNullException(nameof(taskEntity), "Task cannot be null");

            var result = await _context.TaskEntity.AddAsync(taskEntity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public TaskEntity DeleteTaskAsync(TaskEntity taskEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TaskEntity>> GetAllTasksAsync()
        {
            return await _context.TaskEntity
                .OrderBy(task => task.Title)
                .Select(task => new TaskEntity
                {
                    Id = task.Id,
                    Title = task.Title,
                    CreateDate = task.CreateDate,
                    Description = task.Description,
                }).ToListAsync();
        }

        public TaskEntity UpdateTaskAsync(TaskEntity taskEntity)
        {
            throw new NotImplementedException();
        }
    }
}