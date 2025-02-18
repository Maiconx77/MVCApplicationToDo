using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCApplicationToDo.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [DisplayName("Descrição da Tarefa")]
        [StringLength(255)] // Limite máximo de 255 caracteres
        public required string Title { get; set; }

        [DisplayName("Peso")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Weight { get; set; }

        // Chave estrangeira para Milestone Chain
        public int MilestoneChainId { get; set; }

        [DisplayName("MS Chain")]
        public required MilestoneChain MilestoneChain { get; set; }

        // Chave estrangeira para Projeto
        public int ProjectId { get; set; }

        [DisplayName("Projeto")]
        public required Project Project { get; set; }

        [DisplayName("Completed")]
        public bool IsCompleted { get; set; }

        public ICollection<Progress> Progresses { get; set; }
    }
}
