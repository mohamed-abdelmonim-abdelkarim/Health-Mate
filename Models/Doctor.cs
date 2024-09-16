using System.ComponentModel.DataAnnotations;

namespace clinimanagmanetsystem.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        [MaxLength(50)]
        public string? FullName { get; set; }
        public string? Specialization { get; set; }
        public string? ContactNumber { get; set; }
        [RegularExpression(@"[a-zA-Z0-9_]+@[a-zA-Z]+.[a-zA-Z]{2,4}")]
        [Required]
        public string? Email { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
