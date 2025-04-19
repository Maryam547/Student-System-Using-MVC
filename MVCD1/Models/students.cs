using System.ComponentModel.DataAnnotations.Schema;

namespace MVCD2.Models
{
    public class students
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public int DepartmentId { get; set; }


        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

        //relations
        public virtual Departments? Department { get; set; }

        public virtual ICollection<Course_Students> Course_Students { get; set; } = new List<Course_Students>();

    }
}
