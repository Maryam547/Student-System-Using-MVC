using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCD2.Models;
using MVCD2.Repo.Crs;
using MVCD2.Repo.Ins;

namespace MVCD2.Controllers
{
    public class CourseController : Controller
    {
        //private readonly CompanyContext db = new CompanyContext();
        ICourseRepo _courseRepo;
        IInstructorRepo _instructorRepo;
        public CourseController(ICourseRepo courseRepo,IInstructorRepo instructorRepo)
        {
            _courseRepo = courseRepo;
            _instructorRepo = instructorRepo;
        }
        //tempdata
        //course/index
        public IActionResult Index()
        {
            TempData["CourseName"] = "ASP.NET Core";
            return View();
        }
        //course/CourseDetails
        public IActionResult CourseDetails()
        {
            string courseName = "Default";
            if (TempData.ContainsKey("CourseName"))
            {
                courseName = TempData["CourseName"].ToString();
                TempData.Keep();

            }
            ViewBag.CourseName = courseName;
            return View();

        }
        //session
        //course/SetSession
        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("CourseName", "C# Advanced");
            return View();
        }
        //course/GetSession
        public IActionResult GetSession()
        {
            string? courseName = HttpContext.Session.GetString("CourseName");
            ViewBag.CourseName = courseName ?? "No session found";
            return View();
        }
        //send data to another controller
        //course/SendToAnotherController
        public IActionResult SendToAnotherController()
        {
            TempData["CourseName"] = "Entity Framework";
            return View();
        }
        [HttpGet]
        public IActionResult AddCourse()
        {
            ViewBag.Instructors = new SelectList(_instructorRepo.GetAllInstructors(), "Id", "FName");
            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(Courses course)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Instructors = new SelectList(_instructorRepo.GetAllInstructors(), "Id", "FName");
                return View(course);
            }

            _courseRepo.AddCourse(course);
            return RedirectToAction("Index");
        }

    }
}


        
    