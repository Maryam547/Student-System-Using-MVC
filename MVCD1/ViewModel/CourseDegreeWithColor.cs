namespace MVCD2.ViewModel
{
    public class CourseDegreeWithColor
    {
        public int Id { get; set; }
        public string StdName { get; set; }
        public string? Image { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        
        public string Color { get; set; }

        public List<string> Courses { get; set; } = new List<string>();
        public List<int> Degree { get; set; } = new List<int> ();


    }
}
