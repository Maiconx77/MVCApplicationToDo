using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVCApplicationToDo.Models
{
    public class Milestone
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Código")] // Título alternativo
        [StringLength(10)] // Limite máximo de 10 caracteres
        public string Code { get; set; }

        [Required]
        [DisplayName("Descrição do Milestone")] // Título alternativo
        public string Title { get; set; }

        // Chave estrangeira para MilestoneChain
        public int MilestoneChainId { get; set; }
        public MilestoneChain MilestoneChain { get; set; }

        // Data prevista para a realização do milestone
        [Required]
        [DisplayName("Data Prevista")]
        public DateTime ExpectedDate { get; set; }

        // Data de realização do milestone
        [DisplayName("Data de Realização")]
        public DateTime? CompletionDate { get; set; }
    }
}