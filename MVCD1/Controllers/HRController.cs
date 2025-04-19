using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCD2.Models;
using MVCD2.Repo.Unit;
using MVCD2.ViewModel;

namespace MVCD2.Controllers
{
    [Authorize(Roles = "HR")]
    public class HRController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HRController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Instructors()
        {

            //var departments = _departmentRepository.GetAllDepartments();
            //return View(departments);
            var Instructors = _unitOfWork.InstructorRepo.GetAllInstructors();
            return View(Instructors);
        }

        public IActionResult Students()
        {
            var Students = _unitOfWork.StudentRepo.GetAllStudents();
            return View(Students);
        }

        [HttpGet]
        public IActionResult EditInstructor(int id)
        {
            var instructor = _unitOfWork.InstructorRepo.GetInstructorById(id);
            if (instructor == null)
            {
                return NotFound();
            }

            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAllDepartments()
        .Select(d => new SelectListItem
        {
            Value = d.Id.ToString(),
            Text = d.DepartmentName
        }).ToList();

            return View(instructor);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditInstructor(Instructors model)
        {
            if (!ModelState.IsValid) return View(model);

            _unitOfWork.InstructorRepo.EditInstructor(model);
            _unitOfWork.Save();
            return RedirectToAction("Instructors");
        }

        public IActionResult DeleteInstructor(int id)
        {
            if (_unitOfWork.InstructorRepo.GetInstructorById(id) != null)
            {
                _unitOfWork.InstructorRepo.DeleteInstructor(id);
                _unitOfWork.Save();
            }
            return RedirectToAction("Instructors");
        }

        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            var student = _unitOfWork.StudentRepo.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }

            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAllDepartments()
        .Select(d => new SelectListItem
        {
            Value = d.Id.ToString(),
            Text = d.DepartmentName
        }).ToList();

            return View(student);
        }

        [HttpPost]
        public IActionResult EditStudent(students model)
        {
            if (!ModelState.IsValid) return View(model);

            _unitOfWork.StudentRepo.EditStudent(model);
            _unitOfWork.Save();
            return RedirectToAction("Students");
        }

        public IActionResult DeleteStudent(int id)
        {
            if (_unitOfWork.StudentRepo.GetStudentById(id) != null)
            {
                _unitOfWork.StudentRepo.DeleteStudent(id);
                _unitOfWork.Save();
            }
            return RedirectToAction("Students");
        }

        public IActionResult ManageStudents()
        {
            var model = new HRDashboardViewModel
            {
                Students = _unitOfWork.StudentRepo.GetAllStudents()
            };
            return View(model);
        }

        public IActionResult ManageInstructors()
        {
            var model = new HRDashboardViewModel
            {
                Instructors = _unitOfWork.InstructorRepo.GetAllInstructors()
            };
            return View(model);
        }
        public IActionResult Dashboard()
        { 
            return View();
        }

        //public IActionResult Index()
        //{
        //}

    }
}
