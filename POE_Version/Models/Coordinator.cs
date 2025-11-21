using System.ComponentModel.DataAnnotations;

namespace PROG.Models
{
    public class Coordinator
    {
        public Coordinator() { } //For EF
        [Key]
        public Guid CoordinatorID { get; set; }
        [Required, StringLength(100)]
        public string? FirstName { get; set; }
        [Required, StringLength(100)]
        public string? LastName { get; set; }

        [Required, StringLength(100)]
        public string? Username { get; set; }

        [Required, StringLength(100)]
        public string? PasswordHash { get; set; }

        public Coordinator(string firstName, string lastName)
        {
            CoordinatorID = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }
        public Claim EscalateClaim(Claim claim)
        {
            claim.ClaimStatus = Claim.Status.Escalated.ToString();
            return claim;
        }

        public Claim ApproveClaim(Claim claim)
        {
            claim.ClaimStatus = Claim.Status.Approved.ToString();
            return claim;
        }

        public Claim RejectClaim(Claim claim, string adminComments)
        {
            claim.ClaimStatus = Claim.Status.Rejected.ToString();
            claim.AdminComments = adminComments;
            return claim;
        }
    }
}
