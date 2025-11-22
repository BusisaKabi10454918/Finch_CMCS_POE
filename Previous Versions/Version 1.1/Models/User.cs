using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace PROG_PART2.Models
{
    public class User
    {
        [Required]
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]

        public string Password { get; set; }

        public User() { } // EF Core needs this

        public User(string username, string password)
        {
            Username = username;
            Password = BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
