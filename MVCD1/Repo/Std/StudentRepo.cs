using MVCD2.Context;
using MVCD2.Models;
using Microsoft.EntityFrameworkCore;

namespace MVCD2.Repo.Std
{
    public class StudentRepo : IStudentRepo
    {
        CompanyContext db;
        public StudentRepo(CompanyContext _db) 
        {
            //db = new CompanyContext();
            db = _db;
        }

        public ICollection<students> GetAllStudents()
        {
            var std = db.students.Include(s=>s.Department).ToList();
            return std;
        }

        public students GetStudentById(int id)
        {
            var std = db.students.Include(s=>s.Department)
                                 .Include(s=>s.Course_Students)
                                 .ThenInclude(s=>s.Course)
                                 .FirstOrDefault(s=>s.Id==id);
            return std;
        }
        public ICollection<students> GetStudentsByDepartment(int departmentId)
        {
            var query = db.students.Include(s => s.Department).AsQueryable();

            if (departmentId > 0)
            {
                query = query.Where(s => s.DepartmentId == departmentId);
            }

            return query.ToList();
        }
        public void AddStudentWithCourses(students student, List<int> selectedCourses)
        {
            Console.WriteLine($"Adding Student: {student.Name}, ID: {student.Id}");

            db.students.Add(student);
            db.SaveChanges();

            Console.WriteLine($"Saved Student: {student.Id}");

            if (selectedCourses?.Any() == true)
            {
                foreach (var courseId in selectedCourses)
                {
                    Console.WriteLine($"Adding Course ID: {courseId} to Student ID: {student.Id}");
                }

                var courseStudents = selectedCourses.Select(courseId => new Course_Students
                {
                    StudentId = student.Id,
                    CourseId = courseId
                });

                db.courses_students.AddRange(courseStudents);
                db.SaveChanges();
            }
        }

        public void AddStudent(students std)
        {
            db.students.Add(std);
            db.SaveChanges();
        }

        //public void EditStudent(students std) 
        //{
        //    db.students.Update(std);
        //    db.SaveChanges();
        //}

        public void EditStudent(students std)
        {
            var existingStudent = db.students.Include(s => s.Course_Students)
                                              .FirstOrDefault(s => s.Id == std.Id);

            if (existingStudent != null)
            {
                existingStudent.Name = std.Name;
                existingStudent.Age = std.Age;
                existingStudent.Image = std.Image;
                existingStudent.Address = std.Address;
                existingStudent.DepartmentId = std.DepartmentId;

                db.SaveChanges();
            }
        }

        public void DeleteStudent(int id) 
        {
            var std = GetStudentById(id);
            if (std != null)
            {
                db.courses_students.RemoveRange(std.Course_Students);
                db.students.Remove(std);
                db.SaveChanges();
            }
        }

        public ICollection<Courses> GetCoursesByStudentId(int studentId)
        {

            return db.courses_students
    .Include(sc => sc.Course)
        .ThenInclude(c => c.Instructors)
    .Where(sc => sc.StudentId == studentId)
    .Select(sc => sc.Course)
    .ToList();

            //return db.courses_students.Where()

        }


        public Courses GetStudentCourseById(int studentId, int courseId)
        {
            return db.courses_students
                .Where(sc => sc.StudentId == studentId && sc.CourseId == courseId)
                .Select(sc => sc.Course)
                .FirstOrDefault();
        }

        public void UpdateCourse(Courses course)
        {
            db.courses.Update(course);
        }

        public void EnrollStudentInCourse(int studentId, int courseId)
        {
            var studentCourse = new Course_Students
            {
                StudentId = studentId,
                CourseId = courseId
            };

            db.courses_students.Add(studentCourse);
            db.SaveChanges();
        }

        public void UpdateStudentCourses(int studentId, List<int> selectedCourses)
        {
            var existingCourses = db.courses_students.Where(cs => cs.StudentId == studentId).ToList();

            db.courses_students.RemoveRange(existingCourses.Where(cs => !selectedCourses.Contains(cs.CourseId)));

            var newCourses = selectedCourses
                .Where(courseId => !existingCourses.Any(cs => cs.CourseId == courseId))
                .Select(courseId => new Course_Students { StudentId = studentId, CourseId = courseId });

            db.courses_students.AddRange(newCourses);
            db.SaveChanges();
        }


    }
}
