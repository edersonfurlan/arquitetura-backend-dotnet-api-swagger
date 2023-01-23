using curso.api.Business.Entities;

namespace curso.api.Business.Repositories
{
    public interface ICourseRepository
    {
        void Add(Course course);
        void Commit();
        IList<Course> GetByUserId(int userId);
    }
}
