using MVCD2.Context;
using MVCD2.Models;
using Microsoft.EntityFrameworkCore;
namespace MVCD2.Repo.Crs
{
    public class CourseRepo : ICourseRepo
    {
        CompanyContext db;
        public CourseRepo(CompanyContext _db)
        {
            //db = new CompanyContext();
            db = _db;
        }

        public ICollection<Courses> GetAllCourses()
        {
            var crs = db.courses.ToList();
            return crs;
        }

        //public Courses GetCourseById(int id)
        //{
        //    var crs = db.courses.Find(id);
        //    return crs;
        //}

        public void AddCourse(Courses crs)
        {
            db.courses.Add(crs);
            db.SaveChanges();
        }

        public void EditCourse(Courses crs)
        {
            db.courses.Update(crs);
            db.SaveChanges();
        }

        public void DeleteCourse(int id)
        {
            var crs = GetCourseById(id);
            if (crs != null)
            {
                db.courses.Remove(crs);
                db.SaveChanges();
            }
        }

        public ICollection<Courses> GetCoursesByInstructorId(int instructorId)
        {
            return db.courses
       .Where(c => c.InstructorId == instructorId)
       .Include(c => c.Instructors) 
       .ToList();
        }

        public Courses GetCourseById(int courseId)
        {
            return db.courses
                .Include(c => c.Instructors) 
                .FirstOrDefault(c => c.Id == courseId);
        }

    }
}
