using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaskList.Model;

namespace TaskList.DTOs
{
    public class TaskDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Titulo não pode ser vazio...")]
        [StringLength(100, ErrorMessage = "O titulo informado ultrapassa os limites de 100 caracteres...")]
        public string Title { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "O texto informado ultrapassa os limites de 300 caracteres...")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data não pode ser vazia...")]
        public string Date { get; set; }
        public int Status { get; set; } = (int)StatusTaskEnum.Pending;
        public string UserId { get; set; }
    }
}
