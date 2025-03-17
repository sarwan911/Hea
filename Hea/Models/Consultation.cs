using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Hea.Models
{
    public class Consultation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConsultationId { get; set; }
        [Required(ErrorMessage = "AppointmentId is required")]
        public int AppointmentId { get; set; }
        [Required(ErrorMessage = "DoctorId is required")]
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "Notes are required")]
        public string Notes { get; set; }
        public string Prescription { get; set; }
        public DateOnly ConsultationDate { get; set; }
    }
}