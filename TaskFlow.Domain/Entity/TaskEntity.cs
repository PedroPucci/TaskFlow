using System.Text.Json.Serialization;
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
        
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public UserEntity? User { get; set; }
        [JsonIgnore]
        public CategoryEntity? Category { get; set; }
    }
}