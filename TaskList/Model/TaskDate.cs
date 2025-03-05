using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskList.Model
{
    public class TaskDate
    {
        [Key]
        public int Id { get; set; }
        public int TaskId { get; set; }
        public DateTime Data { get; set; }
        [JsonIgnore]
        public Task? Task { get; set; }
    }
}
