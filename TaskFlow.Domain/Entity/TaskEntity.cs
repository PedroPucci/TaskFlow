using TaskFlow.Domain.Enums;
using TaskFlow.Domain.General;

namespace TaskFlow.Domain.Entity
{
    public class TaskEntity : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskEntityStatus Status { get; set; }

        // Relacionamento com Usuário
        public int UserId { get; set; }
        public UserEntity? User { get; set; }
    }
}