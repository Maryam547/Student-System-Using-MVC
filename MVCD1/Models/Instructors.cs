using MVCD2.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCD2.Models
{
    public class Instructors
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FName { get; set; } = string.Empty;

        [Required]
        public string LName { get; set; } = string.Empty;

        public decimal Salary { get; set; }
        public int Age { get; set; }
        public int DepartmentId { get; set; }


        public virtual Departments? Department { get; set; }

        public string Image { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        public string UserId { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Courses>? courses { get; set; }
    }
}