using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVCApplicationToDo.Models
{
    public class Progress
    {
        public int Id { get; set; }

        // Chave estrangeira para Task
        public int TaskId { get; set; }

        // Propriedade de navegação para Task
        public Task Task { get; set; }

        // Chave estrangeira para Milestone
        public int MilestoneId { get; set; }

        // Propriedade de navegação para Milestone
        public Milestone Milestone { get; set; }

        // Status de conclusão
        [Required]
        public bool IsCompleted { get; set; }

        [DisplayName("Data Planejada")]
        public DateTime? PlannedDate { get; set; } // Pode ser nulo

        [DisplayName("Data Prevista")]
        public DateTime? ForecastDate { get; set; } // Pode ser nulo

        [DisplayName("Data Atual")]
        public DateTime? ActualDate { get; set; } // Pode ser nulo

    }
}