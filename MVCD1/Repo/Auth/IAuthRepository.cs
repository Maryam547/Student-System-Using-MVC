using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MVCD2.Models;

namespace MVCD2.Repo.Auth
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterUserAsync(ApplicationUser user, string password, string role);
        Task<ApplicationUser> FindUserByEmailAsync(string email);
        Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe);
        Task SignOutAsync();

        public int? GetLoggedInInstructorId(ClaimsPrincipal user);
        public int? GetLoggedInStudentId(ClaimsPrincipal user);


    }
}
