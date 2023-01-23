using Courses.API.Business.Entities;
using Courses.API.Business.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Courses.API.Infrastructure.Data.Repositories
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

        public async Task<User> GetUserAsync(string login)
        {
            return await _courseDBContext.User.FirstOrDefaultAsync(u => u.Login == login);
        }
    }
}
