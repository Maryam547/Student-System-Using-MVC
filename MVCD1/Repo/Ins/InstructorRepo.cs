using Microsoft.EntityFrameworkCore;
using MVCD2.Context;
using MVCD2.Models;
using NuGet.Packaging;

namespace MVCD2.Repo.Ins
{
    public class InstructorRepo : IInstructorRepo
    {
        CompanyContext db;
        public InstructorRepo(CompanyContext _db)
        {
            //db = new CompanyContext();
            db = _db;
        }

        public ICollection<Instructors> GetAllInstructors()
        {
            var ins = db.instructors.Include(i=>i.Department)
                                    .Include(i=>i.courses).ToList();
            return ins;
        }

        public Instructors GetInstructorById(int id)
        {
            var ins = db.instructors.Include (i=>i.Department)
                                    .Include(i=>i.courses)
                                    .FirstOrDefault(i=>i.Id==id);
            return ins;
        }

        public void AddInstructor(Instructors ins)
        {
            db.instructors.Add(ins);
            db.SaveChanges();
        }

        public void EditInstructor(Instructors ins)
        {
            db.instructors.Update(ins);
            db.SaveChanges();
        }
        //public void EditInstructor(Instructors ins)
        //{
        //    var existingInstructor = db.instructors
        //                                .Include(i => i.courses) 
        //                                .FirstOrDefault(i => i.Id == ins.Id);

        //    if (existingInstructor != null)
        //    {
        //        existingInstructor.FName = ins.FName;
        //        existingInstructor.LName = ins.LName;
        //        existingInstructor.Salary = ins.Salary;
        //        existingInstructor.Age = ins.Age;
        //        existingInstructor.Image = ins.Image;
        //        existingInstructor.HireDate = ins.HireDate;
        //        existingInstructor.DepartmentId = ins.DepartmentId;
        //        db.Entry(existingInstructor).Collection(i => i.courses).Load();
        //        existingInstructor.courses.Clear();
        //        if (ins.courses?.Any() == true)
        //        {
        //            foreach (var course in ins.courses)
        //            {
        //                db.Entry(course).State = EntityState.Unchanged;
        //                existingInstructor.courses.Add(course);
        //            }
        //        }

        //        db.SaveChanges(); 
        //    }
        //}


        public void DeleteInstructor(int id)
        {
            var ins = GetInstructorById(id);
            if (ins != null)
            {
                db.instructors.Remove(ins);
                db.SaveChanges();
            }
        }
    }
}


