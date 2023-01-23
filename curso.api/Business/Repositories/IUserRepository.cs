using curso.api.Business.Entities;

namespace curso.api.Business.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Commit();
        User GetUser(string login);
    }
}
