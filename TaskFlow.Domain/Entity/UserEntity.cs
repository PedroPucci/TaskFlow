using System.Text.Json.Serialization;
using TaskFlow.Domain.General;

namespace TaskFlow.Domain.Entity
{
    public class UserEntity : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsActive { get; set; } = true;

        [JsonIgnore]
        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}
