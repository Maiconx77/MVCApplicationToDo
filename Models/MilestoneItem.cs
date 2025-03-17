using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVCApplicationToDo.Models
{
    public class MilestoneItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Código")] // Acrônimo do milestone
        [StringLength(10)] // Limite máximo de 10 caracteres
        public string Code { get; set; } = string.Empty;

        [Required]
        [DisplayName("Título")] // Acrônimo do milestone
        [StringLength(80)] // Limite máximo de 10 caracteres
        public string Title { get; set; } = string.Empty;

        public int ProjectId { get; set; } // Chave estrangeira para Projeto

        [DisplayName("Projeto")]
        public Project? Project { get; set; } = null;
    }
}
