using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hea.Models
{
    public class DocAvailability
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionId { get; set; }

        [Required(ErrorMessage = "DoctorId is required")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "AvailableDate is required")]
        public DateOnly AvailableDate { get; set; }

        [Required(ErrorMessage = "StartTime is required")]
        public TimeOnly StartTime { get; set; }

        [Required(ErrorMessage = "EndTime is required")]
        public TimeOnly EndTime { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(200, ErrorMessage = "Location can't be longer than 200 characters")]
        public string Location { get; set; }
    }
}