using Courses.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Courses.API.Configurations
{
    public class DbFactoryDbContext : IDesignTimeDbContextFactory<CourseDBContext>
    {
        public CourseDBContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CourseDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            CourseDBContext context = new CourseDBContext(optionsBuilder.Options);

            return context;
        }
    }
}
