using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hea.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "SessionId is required")]
        public int SessionId { get; set; }

        [Required(ErrorMessage = "PatientId is required")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("Booked|Rescheduled|Canceled", ErrorMessage = "Status must be either 'Booked', 'Rescheduled', or 'Canceled'")]
        public string Status { get; set; } // Booked, Rescheduled, Canceled
    }
}