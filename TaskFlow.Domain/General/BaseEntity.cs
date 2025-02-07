using System.Text.Json.Serialization;

namespace TaskFlow.Domain.General
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        [JsonIgnore]
        public DateTime? CreateDate { get; set; }

        [JsonIgnore]
        public DateTime? ModificationDate { get; set; }

        protected BaseEntity()
        {
            CreateDate = DateTime.UtcNow;
        }
    }
}
