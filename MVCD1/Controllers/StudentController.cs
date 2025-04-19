using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCD2.Models;
using MVCD2.Repo.Unit;
using MVCD2.ViewModel;

[Authorize(Roles = "Student")]
public class StudentController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult MyCourses()
    {
        var studentId = _unitOfWork.AuthRepository.GetLoggedInStudentId(User);
        ViewBag.studentId = studentId;
        foreach (var claim in User.Claims)
        {
            Console.WriteLine($"{claim.Type}: {claim.Value}");
        }

        var courses = _unitOfWork.StudentRepo.GetCoursesByStudentId((int)studentId);

        return View(courses);
    }
    [HttpGet]
    public IActionResult EditCourse(int id)
    {
        

        var studentId = _unitOfWork.AuthRepository.GetLoggedInStudentId(User);
        var studentCourses = _unitOfWork.StudentRepo.GetCoursesByStudentId((int)studentId).Select(c => c.Id).ToList();
        var allCourses = _unitOfWork.CourseRepo.GetAllCourses().Select(c => new CourseViewModel
        {
            Id = c.Id,
            Name = c.CourseName,
            Description = c.Topic,
            IsEnrolled = studentCourses.Contains(c.Id) 
        }).ToList();

        return View(allCourses);
    }

    [HttpPost]
    public IActionResult EditCourse(List<int> selectedCourses)
    {
       

        var studentId = _unitOfWork.AuthRepository.GetLoggedInStudentId(User);
        _unitOfWork.StudentRepo.UpdateStudentCourses((int)studentId, selectedCourses);
        _unitOfWork.Save();

        return RedirectToAction("MyCourses");
    }

    public IActionResult StudentDetails(int studentId)
    {
        var student = _unitOfWork.StudentRepo.GetStudentById(studentId);

        if (student == null)
        {
            return NotFound();
        }

        var courses = _unitOfWork.StudentRepo.GetCoursesByStudentId(studentId);

        var viewModel = new StudentDetailsViewModel
        {
            StudentId = student.Id,
            StudentName = student.Name,
            Age = student.Age,
            Address = student.Address,
            Courses = courses.Select(c => new CourseViewModel
            {
                Id = c.Id,
                Name = c.CourseName,
                Description = c.Topic,
                InstructorName = c.Instructors.FName
            }).ToList()
        };

        return View(viewModel);
    }

    //[HttpPost]
    //public IActionResult EnrollInCourse(int courseId)
    //{
    //    var studentId = _unitOfWork.GetUserId(User); 
    //    if (studentId == null) return Unauthorized();

    //    EnrollStudentInCourse(studentId, courseId); 

    //    return RedirectToAction("MyCourses");
    //}

    //public IActionResult AvailableCourses()
    //{
    //    var courses = _unitOfWork.CourseRepo.GetAllCourses()
    //        .Select(c => new CourseViewModel
    //        {
    //            Id = c.Id,
    //            Name = c.CourseName,
    //            Description = c.Topic
    //        })
    //        .ToList();

    //    return View(courses);
    //}

}


//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using MVCD2.Context;
//using MVCD2.Models;
//using MVCD2.Repo.Crs;
//using MVCD2.Repo.Dept;
//using MVCD2.Repo.Std;
//using MVCD2.ViewModel;

//namespace MVCD2.Controllers
//{
//    public class StudentController : Controller
//    {
//        //CompanyContext db = new CompanyContext();
//        IStudentRepo _studentRepo;
//        IDepartmentRepository _departmentRepo;
//        ICourseRepo _courseRepo;
//        public StudentController(IStudentRepo studentRepo,IDepartmentRepository departmentRepo,ICourseRepo courseRepo)
//        {
//            _studentRepo = studentRepo;
//            _departmentRepo = departmentRepo;
//            _courseRepo = courseRepo;
//        }
//        public IActionResult Index()
//        {
//            var std = _studentRepo.GetAllStudents();
//            //foreach (var student in std)
//            //{
//            //    Console.WriteLine($"Student: {student.Name}, Department: {student.Department?.DepartmentName}");
//            //}
//            return View(std);
//        }
//        [HttpGet]
//        public JsonResult GetDepartments()
//        {
//            var departments = _departmentRepo.GetAllDepartments().Select(d => new
//            {
//                id = d.Id,
//                departmentName = d.DepartmentName
//            }).ToList();

//            return Json(departments);
//        }

//        [HttpGet]
//        public JsonResult GetStudentsByDepartment(int departmentId)
//        {
//            var std = _studentRepo.GetStudentsByDepartment(departmentId);
//            return Json(std);

//        }



//        public IActionResult Details(int id)
//        {
//            var std = _studentRepo.GetStudentById(id);
//            return View(std);
//        }

//        public IActionResult DetailsVM(int id)
//        {
//            var std = _studentRepo.GetStudentById(id);
//            if (std == null) return NotFound();

//            var courseDegreeWithColor = new CourseDegreeWithColor
//            {
//                Id = std.Id,
//                StdName = std.Name,
//                Address = std.Address,
//                Age = std.Age,
//                Image = std.Image,
//                Courses = std.course_students.Select(cs => cs.Courses.CourseName).ToList(),
//                Degree = std.course_students.Select(cs => cs.Degree).ToList(),
//                Color = std.course_students.Any(cs => cs.Degree >= 50) ? "green" : "red"
//            };
//            return View(courseDegreeWithColor);

//        }
//        [HttpGet]
//        public IActionResult Delete()
//        {
//            var std = _studentRepo.GetAllStudents();
//            return View(std);
//        }
//        [HttpPost]
//        public IActionResult Delete(int id)
//        {
//            var student = _studentRepo.GetStudentById(id);
//            if (student == null) return NotFound();
//            _studentRepo.DeleteStudent(id);

//            return RedirectToAction("Index");

//        }
//        [HttpGet]
//        public IActionResult AddStudent()
//        {
//            ViewBag.depts = new SelectList(_departmentRepo.GetAllDepartments(), "Id", "DepartmentName");
//            ViewBag.Courses = _courseRepo.GetAllCourses()
//                        .Select(c => new SelectListItem
//                        {
//                            Value = c.Id.ToString(),
//                            Text = c.CourseName
//                        })
//                        .ToList();

//            return View(new students());
//        }

//        [HttpPost]
//        public IActionResult AddStudent(students student, List<int> selectedCourses)
//        {
//            if (!ModelState.IsValid)
//            {
//                ViewBag.depts = new SelectList(_departmentRepo.GetAllDepartments(), "Id", "DepartmentName");
//                ViewBag.Courses = _courseRepo.GetAllCourses()
//                    .Select(c => new SelectListItem
//                    {
//                        Value = c.Id.ToString(),
//                        Text = c.CourseName
//                    })
//                    .ToList();
//                return View(student);
//            }

//            _studentRepo.AddStudentWithCourses(student, selectedCourses);
//            return RedirectToAction("Index");

//        }
//    }
//}