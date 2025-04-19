
namespace MVCD2.Models
{
    public class Departments
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDescription { get; set; }
        public string Location { get; set; }
        //relations
        public ICollection<students> students { get; set; } = new List<students>();
        public virtual ICollection<Instructors>? Instructors { get; set; }
    }
}
