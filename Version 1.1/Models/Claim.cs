using System.ComponentModel.DataAnnotations;


namespace PROG_PART2.Models
{
    public class Claim
    {
        public Claim() { } // EF Core needs this

        public Claim(IndependentLecturer Claimant, Module module, int claimedHours, List<string> SupportingDocs, DateOnly Start, DateOnly End)
        {
            ClaimID = Guid.NewGuid().ToString();
            this.Claimant = Claimant;
            this.ClaimedModule = module;
            this.ClaimedHours = claimedHours;
            this.SuppDocs = SupportingDocs;
            this.ClaimPeriodStart = Start;
            this.ClaimPeriodEnd = End;
        }
        [Required]
        [Key]
        public string ClaimID { get; set; }
        [Required]
        public IndependentLecturer Claimant { get; set; }
        [Required]
        public Module ClaimedModule { get; set; }
        [Required]
        public int ClaimedHours { get; set; }
        
        public decimal ClaimAmount => ClaimedModule.LecturerRate * ClaimedHours;

        /* List of file paths to supporting documents 
         which is initialised empty in case documents aren't prepared yet*/
        public List<string> SuppDocs { get; set; } = new();

        /* All claims start with a 'Pending' status */
        public ClaimStatus.Status Status { get; set; } = ClaimStatus.Status.Pending;

        /* if a claim is rejected or disputed, additional info can be provided
         * by the academic manager or programme coordinator */
        public string AdditionalInfo { get; set; } = string.Empty;
        [Required]
        public DateOnly ClaimPeriodStart { get; set; }
        [Required]
        public DateOnly ClaimPeriodEnd { get; set; }
        public string GenClaimID()
        {
            string startDate = ClaimPeriodStart.ToString("yyMM");
            string endDate = ClaimPeriodEnd.ToString("yyMM");
            return $"{Claimant.FirstName[0]}{Claimant.LastName[0]}-{ClaimPeriodStart:yyMM}/{ClaimPeriodEnd:yyMM}-{ClaimedModule.ModuleCode}";
        }
        public string AddAdditionalInfo(string info)
        {
            AdditionalInfo = info;
            return AdditionalInfo;
        }
    }
}
