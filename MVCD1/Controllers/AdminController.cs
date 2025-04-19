using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCD2.Enums;
using MVCD2.Models;
using MVCD2.Repo.Dept;
using MVCD2.Repo.Unit;
using MVCD2.ViewModel;

namespace MVCD2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartmentRepository _departmentRepository;

        public AdminController(IUnitOfWork unitOfWork,IDepartmentRepository departmentRepository)
        {
            _unitOfWork = unitOfWork;
            _departmentRepository = departmentRepository;
        }

        
        public IActionResult Departments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAllDepartments();
            return View(departments);
        }

        [HttpGet]
        public IActionResult AddDepartment()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddDepartment(Departments model)
        {
            if (!ModelState.IsValid) return View(model);

            _unitOfWork.DepartmentRepository.AddDepartment(model);
            _unitOfWork.Save();
            return RedirectToAction("Departments");
        }

        public IActionResult DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetDepartmentById(id);
            if (department != null)
            {
                _unitOfWork.DepartmentRepository.DeleteDepartment(id);
                _unitOfWork.Save();
            }
            return RedirectToAction("Departments");
        }

        public IActionResult Users()
        {
            var users = _unitOfWork.UserRepository.GetAll();
            return View(users);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser
            {
                FullName = model.UserName,
                Email = model.Email,
                UserName = model.Email
            };

            if (!Enum.IsDefined(typeof(UserRole), model.Role))
            {
                ModelState.AddModelError("", "Role is required.");
                return View(model);
            }

            var result = await _unitOfWork.AuthRepository.RegisterUserAsync(user, model.Password, model.Role.ToString());

            if (result.Succeeded)
                return RedirectToAction("Users");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            await _unitOfWork.UserRepository.DeleteUserAsync(id);
            return RedirectToAction("Users");
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
