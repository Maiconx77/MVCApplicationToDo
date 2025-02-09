using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVCApplicationToDo.Models
{
    public class Milestone
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Código")] // Acrônimo do milestone
        [StringLength(10)] // Limite máximo de 10 caracteres
        public string Code { get; set; }

        [Required]
        [DisplayName("Descrição do Milestone")] // Título descritivo
        public string Title { get; set; }

        // Chave estrangeira para MilestoneChain
        public int MilestoneChainId { get; set; }
        public MilestoneChain MilestoneChain { get; set; }

    }
}