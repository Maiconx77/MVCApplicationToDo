using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCApplicationToDo.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [DisplayName("Descrição da Tarefa")]
        [StringLength(255)]
        public required string Title { get; set; } = string.Empty;
        public required string TaskNumber { get; set; } = string.Empty;

        [DisplayName("Peso")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Weight { get; set; }

        [DisplayName("Completed")]
        public bool IsCompleted { get; set; }

        public int MilestoneChainId { get; set; }
        public MilestoneChain? MilestoneChain { get; set; } = null;
        public  int ProjectId { get; set; }
        public Project? Project { get; set; }
        public ICollection<Progress>? Progresses { get; set; } 
    }
}
