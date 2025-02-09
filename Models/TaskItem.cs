using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCApplicationToDo.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Descrição da Tarefa")] 
        public string Title { get; set; }
        
        public decimal Weight { get; set; }

        [DisplayName("Concluído")]
        public bool IsCompleted { get; set; }

        public int MilestoneChainId { get; set; }

        [DisplayName("MS Chain")]
        public MilestoneChain MilestoneChain { get; set; }
        public ICollection<Progress> Progresses { get; set; }
    }
}
