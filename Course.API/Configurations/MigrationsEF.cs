using Courses.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Courses.API.Configurations
{
    public static class EntityFrameworkExtensions
    {
        public static IApplicationBuilder UseApplyMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var courseDbContext = serviceScope.ServiceProvider.GetService<CourseDBContext>())
                {
                    var pendingMigrations = courseDbContext.Database.GetPendingMigrations();

                    if (pendingMigrations.Count() == 0)
                        return app;

                    courseDbContext.Database.Migrate();
                }
            }

            return app;
        }
    }
}
