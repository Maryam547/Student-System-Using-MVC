using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCD2.Repo.Unit;

namespace MVCD2.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class InstructorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstructorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Courses()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return Unauthorized("User is not authenticated.");
            }
            var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            if (roles.Count == 0)
            {
                return Unauthorized("User has no roles assigned.");
            }

            if (!roles.Contains("Instructor"))
            {
                return Unauthorized("User does not have the required role: Instructor.");
            }

            var instructorId = _unitOfWork.AuthRepository.GetLoggedInInstructorId(User);

            if (instructorId == null)
            {
                return Unauthorized("Instructor ID not found."); 
            }

            var courses = _unitOfWork.CourseRepo.GetCoursesByInstructorId(instructorId.Value);

            return View(courses);
        }

        public IActionResult CourseDetails(int id)
        {
            var course = _unitOfWork.CourseRepo.GetCourseById(id);

            if (course == null)
            {
                return NotFound("Course not found.");
            }

            return View(course);
        }

    }
}



//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using MVCD2.Context;
//using MVCD2.Models;
//using MVCD2.Repo.Crs;
//using MVCD2.Repo.Dept;
//using MVCD2.Repo.Ins;
//using MVCD2.ViewModel;

//namespace MVCD2.Controllers
//{
//    public class InstructorController : Controller
//    {
//        //private readonly CompanyContext db = new CompanyContext();
//        IInstructorRepo _instructorRepo;
//        IDepartmentRepository _departmentRepo;
//        ICourseRepo _courseRepo;
//        public InstructorController(IInstructorRepo instructorRepo,IDepartmentRepository departmentRepo,ICourseRepo courseRepo)
//        {
//            _instructorRepo = instructorRepo;
//            _departmentRepo = departmentRepo;
//            _courseRepo = courseRepo;
//        }
//        public IActionResult Index()
//        {
//            var instructors = _instructorRepo.GetAllInstructors();
//            return View(instructors);
//        }

//        public IActionResult DetailsVM(int id)
//        {
//            var ins = _instructorRepo.GetInstructorById(id);

//            if (ins == null) return NotFound();

//            var viewModel = new ViewInstructorsAndHisCourses
//            {
//                Id = ins.Id,
//                FullName = $"{ins.FName} {ins.LName}",
//                Image = ins.Image,
//                HD = ins.HireDate,
//                Courses = ins.courses.Select(cs => cs.CourseName).ToList()
//            };

//            return View(viewModel);
//        }

//        public IActionResult InstructorNameUniqueDepartment(string FName, string LName, int DepartmentId, int? id)
//        {
//            bool exist = _instructorRepo.GetAllInstructors()
//                .Any(ins => ins.FName.ToLower() == FName.ToLower()
//                         && ins.LName.ToLower() == LName.ToLower()
//                         && ins.DepartmentId == DepartmentId
//                         && (id == null || ins.Id != id));

//            return Json(exist ? $"Instructor {FName} {LName} already exists." : (object)true);
//        }

//        [HttpGet]
//        public IActionResult Add()
//        {
//            ViewBag.Action = "Add";
//            ViewBag.depts = new SelectList(_departmentRepo.GetAllDepartments(), "Id", "DepartmentName");
//            ViewBag.Courses = _courseRepo.GetAllCourses().ToList();
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Add(Instructors instructor, List<int> courses)
//        {
//            if (!ModelState.IsValid)
//            {
//                ViewBag.Action = "Add";
//                ViewBag.depts = new SelectList(_departmentRepo.GetAllDepartments(), "Id", "DepartmentName");
//                ViewBag.Courses = _courseRepo.GetAllCourses().ToList();
//                return View(instructor);
//            }

//            instructor.courses = courses?.Any() == true
//                ? _courseRepo.GetAllCourses().Where(c => courses.Contains(c.Id)).ToList()
//                : new List<Courses>();

//            _instructorRepo.AddInstructor(instructor);
//            return RedirectToAction("Index");
//        }

//        [HttpGet]
//        public IActionResult Edit(int id)
//        {
//            var ins = _instructorRepo.GetInstructorById(id);

//            if (ins == null) return NotFound();

//            ViewBag.Action = "Edit";
//            ViewBag.depts = new SelectList(_departmentRepo.GetAllDepartments(), "Id", "DepartmentName", ins.DepartmentId);
//            ViewBag.Courses = _courseRepo.GetAllCourses().ToList();
//            ViewBag.SelectedCourses = ins.courses.Select(c => c.Id).ToList();

//            return View(ins);
//        }

//        [HttpPost]
//        public IActionResult Edit(Instructors ins, List<int> courses)
//        {
//            if (!ModelState.IsValid)
//            {
//                ViewBag.Action = "Edit";
//                ViewBag.depts = new SelectList(_departmentRepo.GetAllDepartments(), "Id", "DepartmentName", ins.DepartmentId);
//                ViewBag.Courses = _courseRepo.GetAllCourses().ToList();
//                ViewBag.SelectedCourses = courses;
//                return View(ins);
//            }
//            ins.courses = courses?.Any() == true ? _courseRepo.GetAllCourses().Where(c => courses.Contains(c.Id)).ToList():new List<Courses>();
//            _instructorRepo.EditInstructor(ins);
//            return RedirectToAction("Index");
//            //var instructor = _instructorRepo.GetInstructorById(ins.Id);


//            //if (instructor == null) return NotFound();

//            //instructor.FName = ins.FName;
//            //instructor.LName = ins.LName;
//            //instructor.Salary = ins.Salary;
//            //instructor.Age = ins.Age;
//            //instructor.Image = ins.Image;
//            //instructor.HireDate = ins.HireDate;
//            //instructor.DepartmentId = ins.DepartmentId;

//            //instructor.courses.Clear();
//            //if (courses?.Any() == true)
//            //{
//            //    instructor.courses = _courseRepo.GetAllCourses().Where(c => courses.Contains(c.Id)).ToList();
//            //}
//            //_instructorRepo.EditInstructor(instructor);
//            //return RedirectToAction("Index");
//        }
//    }
//}
