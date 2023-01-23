using curso.api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace curso.api.Configurations
{
    public class DbFactoryDbContext : IDesignTimeDbContextFactory<CourseDBContext>
    {
        public readonly IConfiguration _configuration;

        public DbFactoryDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CourseDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CourseDBContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            CourseDBContext context = new CourseDBContext(optionsBuilder.Options);

            return context;
        }
    }
}
