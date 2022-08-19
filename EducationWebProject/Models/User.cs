namespace EducationWebProject.Models
{
    public class User
    {
        public int UserId { get; set; }
        //public string Name { get; set; }
        public string Email { get; set; } = String.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        //public string Email { get; set; }
        public List<Course>? Courses { get; set; }
        public bool IsDeleted { get; set; }
    }
}
