using MVCD2.Models;

namespace MVCD2.Repo.Std
{
    public interface IStudentRepo
    {
        public ICollection<students> GetAllStudents();
        public students GetStudentById(int id);
        public ICollection<students> GetStudentsByDepartment(int departmentId);
        public void AddStudentWithCourses(students student, List<int> selectedCourses);
        public void AddStudent(students std);
        public void EditStudent(students std);
        public void DeleteStudent(int id);

        public ICollection<Courses> GetCoursesByStudentId(int studentId);
        public Courses GetStudentCourseById(int studentId, int courseId);
        public void UpdateCourse(Courses course);

        public void EnrollStudentInCourse(int studentId, int courseId);

        public void UpdateStudentCourses(int studentId, List<int> selectedCourses);

    }
}
