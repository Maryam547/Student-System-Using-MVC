using MVCD2.Models;

namespace MVCD2.Repo.Crs
{
    public interface ICourseRepo
    {
        public ICollection<Courses> GetAllCourses();
        //public Courses GetCourseById(int id);
        public void AddCourse(Courses crs);
        public void EditCourse(Courses crs);
        public void DeleteCourse(int id);

        ICollection<Courses> GetCoursesByInstructorId(int instructorId);

        public Courses GetCourseById(int courseId);


    }
}
