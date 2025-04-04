using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVCApplicationToDo.Models
{
    public class MilestoneChain
    {
        public int Id { get; set; }

        [DisplayName("Código")] 
        [StringLength(5)] 
        public required string Code { get; set; }

        [DisplayName("Descrição")]
        [StringLength(255)]
        public required string Title { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; } = null;
        public List<Milestone>? Milestones { get; set; } = new List<Milestone>();
        public ICollection<TaskItem>? TaskItems { get; set; }
    }
}