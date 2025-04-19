using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using MVCD2.Models;
namespace MVCD2.Context
{
    public class CompanyContext : IdentityDbContext<ApplicationUser>
    {
        public CompanyContext(DbContextOptions<CompanyContext> op):base(op) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
            //used only with core
            //optionsBuilder.UseSqlServer("Server = .;Database = CompanyBanha;Encrypt = false;Trusted_Connection = true");

            //core & framework
            //optionsBuilder.UseSqlServer("");

        //}
        public DbSet<students>? students { get; set; }

        public DbSet<Departments>? departments { get; set; }

        public DbSet<Instructors>? instructors { get; set; }

        public DbSet<Courses>? courses { get; set; }
        public DbSet<Course_Students>? courses_students { get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course_Students>()
        .HasKey(cs => new { cs.CourseId, cs.StudentId });

            modelBuilder.Entity<Course_Students>()
                .HasOne(cs => cs.Course)
                .WithMany(c => c.Course_Students)
                .HasForeignKey(cs => cs.CourseId);

            modelBuilder.Entity<Course_Students>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.Course_Students)
                .HasForeignKey(cs => cs.StudentId);

            modelBuilder.Entity<students>()
        .HasOne(s => s.User)
        .WithOne(u => u.Student)
        .HasForeignKey<students>(s => s.UserId)
        .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
