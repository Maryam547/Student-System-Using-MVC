using MVCD2.Models;

namespace MVCD2.Repo.User
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetAll();
        Task<ApplicationUser?> GetByIdAsync(string id);
        Task DeleteUserAsync(string id);
    }
}
