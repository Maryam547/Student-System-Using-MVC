namespace MVCD2.ViewModel
{
    public class StudentDetailsViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public int Age { get; set; }
        public string? Address { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}
