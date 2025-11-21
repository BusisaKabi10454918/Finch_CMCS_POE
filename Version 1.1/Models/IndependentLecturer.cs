using System.ComponentModel.DataAnnotations;


namespace PROG_PART2.Models
{
    public class IndependentLecturer
    {
        [Required]
        [Key]
        [StringLength(10)]
        public string LecturerID { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        public List<Module> Modules { get; set; } = new();
        public List<Claim> Claims { get; set; } = new();

        public void UploadSuppDocs(Claim claim, List<string> newDocs)
        {
            claim.SuppDocs.AddRange(newDocs);
        }

        public Claim CreateClaim(IndependentLecturer lecturer, Module module, int claimedHours, List<string> suppDocs, DateOnly start, DateOnly end)
        {
            Claim newClaim = new Claim(lecturer, module, claimedHours, suppDocs, start, end);
            lecturer.Claims.Add(newClaim);
            return newClaim;
        }
    }
}
