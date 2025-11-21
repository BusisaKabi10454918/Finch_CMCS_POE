using System.ComponentModel.DataAnnotations;

namespace PROG_PART2.Models
{
    public class AcademicManager
    {
        [Required]
        [Key]
        [StringLength(10)]
        public int ManagerId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
