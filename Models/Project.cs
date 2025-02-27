using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVCApplicationToDo.Models
{
    public class Project
    {
        public int Id { get; set; }

        [DisplayName("Código")]
        [StringLength(10)]
        public required string Code { get; set; }

        [DisplayName("Nome do Projeto")]
        public required string Title { get; set; }

        public ICollection<TaskItem>? TaskItems { get; set; } 
        public ICollection<Progress>? Progresses { get; set; } 
    }
}
