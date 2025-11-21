using System.ComponentModel.DataAnnotations;
namespace PROG.Models
{
    public class Contract
    {
        public Contract() { } //For EF

        [Key]
        public Guid ContractID { get; set; } = Guid.NewGuid();

        //List of the assigned modules, so codes can be checked when a claim is made.
        //Will auto create, empty when a lecturer is created.
        [Required, StringLength(100)]
        public List<string> AssignedModules { get; set; }

        //Signed Lecturer
        [Required]
        private Guid LecturerID { get; set; }

        [Required]
        [StringLength(100)]
        public String ContractedLecturer { get; set; }

        //Maximum claimable per month
        [Required]
        public decimal MaximumClaimableAmount { get; set; }

        [Required]
        public DateOnly ExpiryDate { get; set; } = new DateOnly(DateTime.Now.Year, 12, 31);

        public string GetAssignedModulesAsString()
        {
            return string.Join(", ", AssignedModules);
        }

        public Contract(List<string> assignedModules, Guid lecturerID, String signatory, decimal maximumClaimableAmount)
        {
            AssignedModules = assignedModules;
            LecturerID = lecturerID;
            ContractedLecturer = signatory;
            MaximumClaimableAmount = maximumClaimableAmount;
        }

        public void AdjustMaximumClaimableAmount(decimal newAmount)
        {
            MaximumClaimableAmount = newAmount;
        }

        public void AddAssignedModule(string moduleCode)
        {
            AssignedModules.Add(moduleCode);
        }

        public void RemoveAssignedModule(string moduleCode)
        {
            AssignedModules.Remove(moduleCode);
        }
    }
}
