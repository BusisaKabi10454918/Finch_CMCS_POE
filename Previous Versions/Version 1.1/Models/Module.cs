using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG_PART2.Models
{
    public class Module
    {
        [Required]
        [Key]
        [StringLength(10)]
        public string ModuleCode { get; set; }
        [Required] [StringLength(10)]
        public string ModuleName { get; set; }
        [Required]
        [Range(0, 1000)]
        public decimal LecturerRate { get; set; }

        [ForeignKey("Lecturer")]
        public string LecturerID { get; set; }
        public IndependentLecturer Lecturer { get; set; }
    }
}
