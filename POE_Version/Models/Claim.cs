using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace PROG.Models
{
    public class Claim
    {
        
        //Primary Key
        [Required, Key]
        public Guid ClaimID { get; set; }

        [Required]
        public string ClaimReadID { get; set; }
        [Required, StringLength(100)]
        public string? ModuleCode { get; set; }
        [Required]
        public Guid LecturerID { get; set; }
        //Navigation Property
        public Lecturer? Lecturer { get; set; }
        [Required]
        public int ClaimedHours { get; set; }
        [Required]
        public decimal ClaimAmount { get; set; }
        [Required]
        public DateTime SubmissionDate { get; set; }
        [Required]
        public DateOnly ClaimPeriodStart { get; set; }
        [Required]
        public DateOnly ClaimPeriodEnd { get; set; }
        [Required]
        public string SupportingDocuments { get; set; } = string.Empty;
        public String ClaimStatus { get; set; } = (Status.Pending).ToString();
        public string? AdminComments { get; set; }

        public Claim() { } //For EF
        public Claim(string moduleCode, Guid lecturerID, Lecturer lecturer, int claimedHours, decimal claimAmount, DateTime submissionDate,
                     DateOnly claimPeriodStart, DateOnly claimPeriodEnd, string supportingDocuments)
        {
            ClaimID = Guid.NewGuid();
            ModuleCode = moduleCode;
            LecturerID = lecturerID;
            this.Lecturer = lecturer;
            this.ClaimedHours = claimedHours;
            ClaimAmount = claimAmount;
            this.SubmissionDate = submissionDate;
            this.ClaimPeriodStart = claimPeriodStart;
            this.ClaimPeriodEnd = claimPeriodEnd;
            this.SupportingDocuments = supportingDocuments;
        }
        public enum Status
        {
            Pending,
            Approved,
            Rejected,
            Escalated
        }

        public static string GenerateClaimReadID(string moduleCode, DateOnly submissionDate, int number, Lecturer lecturer) 
        {
            //This ID is so that a reviewer, accept, reject or escalate a claim can easily identify it
            return $"{moduleCode}-{submissionDate:yyyyMMdd}-{lecturer.FirstName}-{number}-";
        }
    }
}
