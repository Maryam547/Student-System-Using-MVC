using MVCD2.Models;

namespace MVCD2.Repo.Ins
{
    public interface IInstructorRepo
    {
        public ICollection<Instructors> GetAllInstructors();
        public Instructors GetInstructorById(int id);
        public void AddInstructor(Instructors ins);
        public void EditInstructor(Instructors ins);
        public void DeleteInstructor(int id);
    }
}
