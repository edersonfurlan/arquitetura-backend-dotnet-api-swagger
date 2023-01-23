using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using Microsoft.EntityFrameworkCore;

namespace curso.api.Infrastructure.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseDBContext _courseDBContext;

        public CourseRepository(CourseDBContext courseDBContext)
        {
            _courseDBContext = courseDBContext;
        }

        public void Add(Course course)
        {
            _courseDBContext.Course.Add(course);
        }

        public void Commit()
        {
            _courseDBContext.SaveChanges();
        }

        public IList<Course> GetByUserId(int userId)
        {
            return _courseDBContext.Course.Include(i => i.User).Where(c => c.UserId == userId).ToList();
        }
    }
}
