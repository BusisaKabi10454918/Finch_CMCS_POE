using System.ComponentModel.DataAnnotations;

namespace PROG_PART2.Models
{
    public static class ClaimStatus
    {
        public enum Status
        {
            Pending,
            Approved,
            Rejected
        }
    }
}
