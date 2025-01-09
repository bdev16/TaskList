using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace TaskList.Model
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O Titulo não pode ser vazio...")]
        [StringLength(100, ErrorMessage = "O titulo informado ultrapassa os limites de 100 caracteres...")]
        public string Title { get; set; } = string.Empty;
        [StringLength(300, ErrorMessage = "O texto informado ultrapassa os limites de 300 caracteres...")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "A data não pode ser vazia...")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "O Status deve ser definido...")]
        [DataType(DataType.DateTime, ErrorMessage = "A Data informada não segue o formato 00/00/0000 00:00:00")]
        public string Status { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
