using MVCD2.Models;

namespace MVCD2.Repo.Dept
{
    public interface IDepartmentRepository
    {
        public ICollection<Departments> GetAllDepartments();
        public Departments GetDepartmentById(int id);
        public void AddDepartment(Departments dept);
        public void EditDepartment(Departments dept);
        public void DeleteDepartment(int id);
    }
}
