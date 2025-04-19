using System.ComponentModel.DataAnnotations;
using MVCD2.Context;

namespace MVCD2.Validators
{
    public class UniqueCourseNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string name = value as string;
            CompanyContext db = validationContext.GetRequiredService<CompanyContext>();
            var CrsFromDb = db.courses.FirstOrDefault(c => c.CourseName == name);
            if (CrsFromDb == null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Name Already Exist");

        }
    }
}
