using System.Text.Json.Serialization;
using TaskFlow.Domain.General;

namespace TaskFlow.Domain.Entity
{
    public class CategoryEntity : BaseEntity
    {
        public string? Name { get; set; }
        [JsonIgnore]
        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}