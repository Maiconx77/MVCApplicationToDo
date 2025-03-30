using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCApplicationToDo.Models
{
    public class Milestone
    {
        public int Id { get; set; }

        [DisplayName("Código")] // Acrônimo do milestone
        [StringLength(10)] // Limite máximo de 10 caracteres
        public required string Code { get; set; }

        [DisplayName("Descrição do Milestone")] // Título descritivo
        [StringLength(255)] // Limite máximo de 255 caracteres
        public required string Title { get; set; }

        [DisplayName("Progresso")] // Título descritivo
        [Range(0, 100, ErrorMessage = "O valor deve estar entre 0 e 100.")]
        public int Value { get; set; }
        public int Order { get; set; }

        // Chave estrangeira para MilestoneChain
        public int MilestoneChainId { get; set; }
        public MilestoneChain? MilestoneChain { get; set; }

    }
}