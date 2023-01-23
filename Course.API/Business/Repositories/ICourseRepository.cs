using Courses.API.Business.Entities;

namespace Courses.API.Business.Repositories
{
    public interface ICourseRepository
    {
        void Add(Course course);
        void Commit();
        IList<Course> GetByUserId(int userId);
    }
}
