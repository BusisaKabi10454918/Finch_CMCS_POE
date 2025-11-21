using System.ComponentModel.DataAnnotations;

namespace PROG_PART2.Models
{
    public class ProgrammeCoordinator
    {
        [Required]
        [Key]
        [StringLength(10)]
        public string CoordinatorId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public static Claim ApproveClaim(Claim claim)
        {
            claim.Status = ClaimStatus.Status.Approved;
            return claim;
        }
        public static Claim RejectClaim(Claim claim)
        {
            claim.Status = ClaimStatus.Status.Rejected;
            return claim;
        }
    }
}
