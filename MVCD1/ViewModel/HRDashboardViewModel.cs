using MVCD2.Models;

namespace MVCD2.ViewModel
{
    public class HRDashboardViewModel
    {
        public IEnumerable<Instructors> Instructors { get; set; }
        public IEnumerable<students> Students { get; set; }
    }
}
