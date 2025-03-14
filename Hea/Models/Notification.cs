using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Hea.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Message { get; set; }
        public DateOnly CreatedAt { get; set; }
        [Required]
        public string Status { get; set; } // Booked, Rescheduled, Canceled, etc.
    }
}