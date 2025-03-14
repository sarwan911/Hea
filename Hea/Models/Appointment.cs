using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hea.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }
        [Required]
        public int SessionId { get; set; }
        [Required]
        public int PatientId { get; set; }
        public string Status { get; set; } // Booked, Rescheduled, Canceled
    }
}