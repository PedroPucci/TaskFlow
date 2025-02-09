using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Dto;
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
            var response = _context.TaskEntity.Remove(taskEntity);
            return response.Entity;
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
                    Status = task.Status,
                }).ToListAsync();
        }

        public async Task<List<TaskDto>> GetTasksWithUserAsync()
        {
            return await _context.TaskEntity
                .Include(t => t.User)
                .OrderBy(t => t.Title)
                .Select(t => new TaskDto
                {
                    TaskId = t.Id,
                    UserId = t.UserId,
                    UserName = t.User.Name,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate
                })
                .ToListAsync();
        }

        public async Task<List<TaskDto>> GetTasksByUserAsync(int userId)
        {
            return await _context.TaskEntity
                .Where(t => t.UserId == userId)
                .Include(t => t.User)
                .OrderBy(t => t.Title)
                .Select(t => new TaskDto
                {
                    TaskId = t.Id,
                    UserId = t.UserId,
                    UserName = t.User.Name,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                })
                .ToListAsync();
        }

        public void UpdateTask(TaskEntity taskEntity)
        {
            _context.TaskEntity.Update(taskEntity);
        }

        public async Task<TaskEntity> GetTaskByIdAsync(int taskId)
        {
            var task = await _context.TaskEntity
                .Where(t => t.Id == taskId)
                .Select(t => new TaskEntity
                {          
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Status = t.Status,                    
                    ModificationDate = t.ModificationDate
                })
                .FirstOrDefaultAsync();

            return task;
        }
    }
}