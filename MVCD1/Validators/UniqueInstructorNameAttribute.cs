using System.ComponentModel.DataAnnotations;
using MVCD2.Context;
using MVCD2.Models;

namespace MVCD2.Validators
{
    public class UniqueInstructorNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var fname = value as string;
            var lname = value as string;
            CompanyContext db = validationContext.GetRequiredService<CompanyContext>();
            Instructors ins = validationContext.ObjectInstance as Instructors;
            var ExistingIns = db.instructors.FirstOrDefault(i =>
            i.FName == fname && 
            i.LName == lname && 
            i.DepartmentId == ins.DepartmentId && 
            i.Id != ins.Id);
            if (ExistingIns != null)
            {
                return new ValidationResult("An Instructor with the same alredy exist iin this department");
            }
            return ValidationResult.Success;

            


        }
    }
}

