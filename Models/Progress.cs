using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCApplicationToDo.Models
{
    public class Progress
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; }
        public int MilestoneId { get; set; }
        public Milestone Milestone { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Weight { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Earned { get; set; }
        // Pode ser nulo
        [DisplayName("Data Planejada")]
        public DateTime? PlannedDate { get; set; } 
        // Pode ser nulo
        [DisplayName("Data Prevista")]
        public DateTime? ForecastDate { get; set; } 
        // Pode ser nulo
        [DisplayName("Data Atual")]
        public DateTime? ActualDate { get; set; } 
        [Required]
        public bool IsCompleted { get; set; }
    }
}