using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MVCD2.Context;
using MVCD2.Models;

namespace MVCD2.Repo.Auth
{
    public class AuthRepository:IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly CompanyContext _db; 

        public AuthRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, CompanyContext db) 
        { 
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public async Task<IdentityResult> RegisterUserAsync(ApplicationUser user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);

                if (role == "Instructor")
                {
                    var instructor = new Instructors
                    {
                        FName = user.FullName.Split(' ')[0], 
                        LName = user.FullName.Split(' ').Length > 1 ? user.FullName.Split(' ')[1] : "", // الاسم الأخير
                        Salary = 50000, 
                        Age = 22, 
                        DepartmentId = 2, 
                        UserId = user.Id 
                    };

                    _db.instructors.Add(instructor);
                    await _db.SaveChangesAsync();

                }
                else if (role == "Student") 
                {
                    var student = new students
                    {
                        Name = user.FullName,
                        Age = 20, 
                        Address = "Not Provided", 
                        DepartmentId = 2, 
                        UserId = user.Id 
                    };

                    _db.students.Add(student);
                    await _db.SaveChangesAsync();

                }


            }
            return result;
        }

        public async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public int? GetLoggedInInstructorId(ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) return null;

            var instructor = _db.instructors.FirstOrDefault(i => i.UserId.ToString() == userId);
            return instructor?.Id;
        }

        public int? GetLoggedInStudentId(ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) return null;

            var student = _db.students.FirstOrDefault(i => i.UserId.ToString() == userId);
            return student?.Id;
        }
    }
}
