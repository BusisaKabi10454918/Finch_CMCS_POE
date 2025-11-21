using System.ComponentModel.DataAnnotations;
namespace PROG.Models
{
    public class Module
    {
        public Module() { } //For EF
        //Primary Key

        [Key]
        public Guid ModuleID { get; set; }

        [Required, StringLength(100)]
        public string? ModuleCode { get; set; }

        [Required, StringLength(200)]
        public string? ModuleName { get; set; }

        [Required, Range(0, 500)]
        public int ModuleRatePerHour { get; set; }

        public Module(string moduleCode, string moduleName, int moduleRatePerHour)
        {
            ModuleID = Guid.NewGuid();
            ModuleCode = moduleCode;
            ModuleName = moduleName;
            ModuleRatePerHour = moduleRatePerHour;
        }
    }
}
