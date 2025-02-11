using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVCApplicationToDo.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Código")] // Código de projeto
        [StringLength(10)] // Limite máximo de 10 caracteres
        public string Code { get; set; }

        [Required]
        [DisplayName("Nome do Projeto")] // Título descritivo
        public string Title { get; set; }
    }
}
