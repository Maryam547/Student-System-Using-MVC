using System.ComponentModel.DataAnnotations.Schema;

namespace MVCD2.Models
{
    public class Course_Students
    {
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public int Degree { get; set; }


        public virtual Courses? Course { get; set; }
        public virtual students? Student { get; set; }
    }
}
