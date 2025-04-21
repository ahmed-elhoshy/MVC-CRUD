using Microsoft.EntityFrameworkCore;

namespace MVC_D8.Models
{
    public class ITIMvcDbContext : DbContext
    {
        public ITIMvcDbContext() { } // Default constructor 
        public ITIMvcDbContext(DbContextOptions options) : base(options) 
        {
        
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-9IE8V4JF;Database=ITIMVC;Integrated Security=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData([
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Instructor" },
                new Role { Id = 3, Name = "Student" }
            ]);
            base.OnModelCreating(modelBuilder);
        }
    }
}
