using System.ComponentModel.DataAnnotations;

namespace PROG.Models
{
    public class Manager
    { 
        public Manager() { } //For EF

        [Key]
        public Guid ManagerID { get; set; }

        [Required, StringLength(100)]
        public string? FirstName { get; set; }
        [Required, StringLength(100)]
        public string? LastName { get; set; }
        [Required, StringLength(100)]
        public string? Username { get; set; }
        [Required, StringLength(100)]
        public string? PasswordHash { get; set; }
        public Manager(string firstName, string lastName, string username, string passwordHash)
        {
            ManagerID = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            PasswordHash = passwordHash;
        }

        public Claim ApproveClaim(Claim claim)
        {
            claim.ClaimStatus = Claim.Status.Approved;
            return claim;
        }

        public Claim RejectClaim(Claim claim, string adminComments)
        {
            claim.ClaimStatus = Claim.Status.Rejected;
            claim.AdminComments = adminComments;
            return claim;
        }

        public Claim AdjustContract(Claim claim)
        {
            // Implementation for adjusting contract. Future work.
            return null;
        }

    }
}
