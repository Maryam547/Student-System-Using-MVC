using MVCD2.Context;
using MVCD2.Models;

namespace MVCD2.Repo.Dept
{
    public class DepartmentRepository : IDepartmentRepository
    {
        CompanyContext db;
        public DepartmentRepository(CompanyContext _db)
        {
            //db = new CompanyContext();
            db = _db;
        }

        public ICollection<Departments> GetAllDepartments()
        {
            var dept = db.departments.ToList();
            return dept;
        }

        public Departments GetDepartmentById(int id)
        {
            var dept = db.departments.Find(id);
            return dept;
        }

        public void AddDepartment(Departments dept)
        {
            db.departments.Add(dept);
            db.SaveChanges();
        }

        public void EditDepartment(Departments dept)
        {
            db.departments.Update(dept);
            db.SaveChanges();
        }

        public void DeleteDepartment(int id)
        {
            var dept = GetDepartmentById(id);
            if (dept != null) 
            {
                db.departments.Remove(dept);
                db.SaveChanges();
            }
        }
    }
}

