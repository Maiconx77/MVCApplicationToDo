using System.ComponentModel;

namespace MVCApplicationToDo.Models
{
    public class Milestone
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Value { get; set; }
        public int Order { get; set; }

        // Chave estrangeira para MilestoneChain
        public int MilestoneChainId { get; set; }
        public MilestoneChain? MilestoneChain { get; set; }

        // Chave estrangeira para MilestoneItem
        public int MilestoneItemId { get; set; }
        public MilestoneItem? MilestoneItem { get; set; }

    }
}