using MVCD2.Repo.Auth;
using MVCD2.Repo.Crs;
using MVCD2.Repo.Dept;
using MVCD2.Repo.Ins;
using MVCD2.Repo.Std;
using MVCD2.Repo.User;

namespace MVCD2.Repo.Unit
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }
        IUserRepository UserRepository { get; }
        IAuthRepository AuthRepository { get; }
        IInstructorRepo InstructorRepo { get; }
        IStudentRepo StudentRepo { get; }
        ICourseRepo CourseRepo { get; }

        void Save();
    }
}
