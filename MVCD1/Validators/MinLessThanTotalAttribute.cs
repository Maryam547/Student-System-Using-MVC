using System.ComponentModel.DataAnnotations;
using MVCD2.Models;

namespace MVCD2.Validators
{
    public class MinLessThanTotalAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Courses? course = validationContext.ObjectInstance as Courses;
            if (course?.MinDegree >= course?.TotalDegree)
            {
                return new ValidationResult("Minimum Degree must be less than Total Degree");
            }

            return ValidationResult.Success;
        }
    }
}
