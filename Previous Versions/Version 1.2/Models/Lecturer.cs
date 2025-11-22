using System.ComponentModel.DataAnnotations;

namespace PROG.Models
{
    public class Lecturer
    {
        public Lecturer() { } //For EF

        //Primary Key
        [Required]
        [Key]
        public Guid LecturerID { get; set; }

        [Required, StringLength(100)]
        public string? FirstName { get; set; }

        [Required, StringLength(100)]
        public string? LastName { get; set; }

        [Required, StringLength(100)]
        public string?Username { get; set; }

        [Required, StringLength(100)]
        public string? PasswordHash { get; set; }

        public int NumberOfClaims { get; set; } = 0;

    }
}
