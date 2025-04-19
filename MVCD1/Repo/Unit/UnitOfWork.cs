using MVCD2.Context;
using MVCD2.Repo.Auth;
using MVCD2.Repo.Crs;
using MVCD2.Repo.Dept;
using MVCD2.Repo.Ins;
using MVCD2.Repo.Std;
using MVCD2.Repo.User;

namespace MVCD2.Repo.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyContext _db;

        //
        public IDepartmentRepository DepartmentRepository { get; }
        public IUserRepository UserRepository { get; }
        public IAuthRepository AuthRepository { get; }

        //
        public IInstructorRepo InstructorRepo { get; }
        public IStudentRepo StudentRepo { get; }

        //
        public ICourseRepo CourseRepo { get; }


        public UnitOfWork(CompanyContext db,
                          IDepartmentRepository departmentRepository,
                          IUserRepository userRepository,
                          IAuthRepository authRepository,
                          IInstructorRepo instructorRepo,
                          IStudentRepo studentRepo,
                          ICourseRepo courseRepo)
        {
            _db = db;
            DepartmentRepository = departmentRepository;
            UserRepository = userRepository;
            AuthRepository = authRepository;
            InstructorRepo = instructorRepo;
            StudentRepo = studentRepo;
            CourseRepo = courseRepo;

        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
