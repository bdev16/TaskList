using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace TaskList.Model
{
    public class Task : IValidatableObject
    {
        public Task () 
        {
            Dates = new Collection<TaskDate>();
        }
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Titulo não pode ser vazio...")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(100, ErrorMessage = "O titulo informado ultrapassa os limites de 100 caracteres...")]
        public string Title { get; set; } = string.Empty;
        [Column(TypeName = "varchar(300)")]
        [StringLength(300, ErrorMessage = "O texto informado ultrapassa os limites de 300 caracteres...")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "A data não pode ser vazia...")]
        [DataType(DataType.DateTime, ErrorMessage = "A Data informada não segue o formato 00/00/0000 00:00:00")]
        public int Status { get; set; } = (int)StatusTaskEnum.Pending;
        public string UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public virtual ICollection<TaskDate> Dates { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            int sizeStatusTaskEnum = Enum.GetValues(typeof(StatusTaskEnum)).Length;
            if (Status < 1 || Status > sizeStatusTaskEnum)
            {
                yield return new
                    ValidationResult("O Status informado não existe...1: Pendente; 2: Concluida.", new[] { nameof(this.Status) });
            }  
        }
    }
}
