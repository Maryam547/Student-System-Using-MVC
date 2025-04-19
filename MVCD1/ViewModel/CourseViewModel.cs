namespace MVCD2.ViewModel
{
    public class CourseViewModel
    {
        public int Id { get; set; }  
        public string? Name { get; set; }  
        public string? Description { get; set; }
        public int StudentId { get; set; }


        public string? InstructorName { get; set; }

        public bool IsEnrolled { get; set; }
    }
}
