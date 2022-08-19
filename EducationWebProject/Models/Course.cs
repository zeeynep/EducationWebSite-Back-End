namespace EducationWebProject.Models
{
    public class Course
    {
        public int CourseId { get; set; }
#nullable enable
        public string? CourseContent { get; set; }
#nullable disable
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
