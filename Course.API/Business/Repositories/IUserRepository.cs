using Courses.API.Business.Entities;

namespace Courses.API.Business.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Commit();
        Task<User> GetUserAsync(string login);
    }
}
