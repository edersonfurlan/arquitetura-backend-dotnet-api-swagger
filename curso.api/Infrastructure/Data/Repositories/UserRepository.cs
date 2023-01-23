using curso.api.Business.Entities;
using curso.api.Business.Repositories;

namespace curso.api.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CourseDBContext _courseDBContext;

        public UserRepository(CourseDBContext courseDBContext)
        {
            _courseDBContext = courseDBContext;
        }

        public void Add(User user)
        {
            _courseDBContext.User.Add(user);
        }

        public void Commit()
        {
            _courseDBContext.SaveChanges();
        }

        public User GetUser(string login)
        {
            return _courseDBContext.User.FirstOrDefault(u => u.Login == login);
        }
    }
}
