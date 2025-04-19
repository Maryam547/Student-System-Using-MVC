using System.ComponentModel.DataAnnotations;
using MVCD2.Validators;
namespace MVCD2.Models
{
    public class Courses
    {

        public int Id { get; set; }
        [Required]
        [UniqueCourseName]
        public string CourseName { get; set; }
        public string Topic { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Minimum degree must be smaller than Total degree")]
        public int MinDegree { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Total degree must be greater than Minimum degree")]
        public int TotalDegree { get; set; }

        [MinLessThanTotal]
        public int ValidatedMinDegree => MinDegree;


        //relations
        [Required]
        public int InstructorId { get; set; }

        public virtual Instructors? Instructors { get; set; }
        public virtual ICollection<Course_Students> Course_Students { get; set; } = new List<Course_Students>();

    }
}
