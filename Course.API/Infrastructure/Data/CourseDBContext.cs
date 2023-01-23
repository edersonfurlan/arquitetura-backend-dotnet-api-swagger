using Courses.API.Business.Entities;
using Courses.API.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Courses.API.Infrastructure.Data
{
    public class CourseDBContext : DbContext
    {
        public CourseDBContext(DbContextOptions<CourseDBContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> User { get; set; }
        public DbSet<Course> Course { get; set; }
    }
}
