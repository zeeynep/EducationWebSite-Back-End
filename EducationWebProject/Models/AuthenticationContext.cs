namespace EducationWebProject.Models
{
    public class AuthenticationContext : DbContext
    {
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer("Server=DESKTOP-A1B53DV;Database=eduwebdb;Trusted_Connection=true");
        }
#nullable enable
        public DbSet<User>? Users { get; set; }
#nullable disable
        public DbSet<Course> Courses { get; set; }
    }
}

