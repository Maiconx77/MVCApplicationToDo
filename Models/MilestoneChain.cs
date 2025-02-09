using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVCApplicationToDo.Models
{
    public class MilestoneChain
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Código")] // Título alternativo
        [StringLength(10)] // Limite máximo de 10 caracteres
        public string Code { get; set; }

        [Required]
        [DisplayName("Descrição do Milestone")] // Título alternativo
        public string Title { get; set; }

        // Propriedade de navegação para a coleção de Milestones
        public ICollection<Milestone> Milestones { get; set; }
    }
}