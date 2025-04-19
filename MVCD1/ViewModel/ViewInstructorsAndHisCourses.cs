namespace MVCD2.ViewModel
{
    public class ViewInstructorsAndHisCourses
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public DateOnly HD { get; set; }
        public List<string> Courses { get; set; } = new List<string>();

    }
}
