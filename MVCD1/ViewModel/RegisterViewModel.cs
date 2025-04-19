using System.ComponentModel.DataAnnotations;
using MVCD2.Enums;

namespace MVCD2.ViewModel
{
    public class RegisterViewModel
    {
        [Required, Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required, Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [EnumDataType(typeof(UserRole), ErrorMessage = "Invalid role")]
        public UserRole Role { get; set; }

        public decimal? Salary { get; set; }  
        public int? Age { get; set; }
        public int? DepartmentId { get; set; }
    }
}
