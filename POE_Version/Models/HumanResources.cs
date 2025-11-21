using System.ComponentModel.DataAnnotations;

namespace PROG.Models
{
    public class HumanResources
    {
        public HumanResources() { } //For EF

        //Primary Key
        [Key]
        public Guid HumanResourcesID { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string? LastName { get; set; }
        [Required]
        [StringLength(100)]
        public string? Username { get; set; }
        [Required]
        [StringLength(100)]
        public string? PasswordHash { get; set; }

        public HumanResources(string firstName, string lastName, string username, string passwordHash)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            PasswordHash = passwordHash;
        }

    }
}
