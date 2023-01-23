using Courses.API.Models.Users;

namespace Courses.API.Configurations
{
    public interface IAuthenticationService
    {
        string GenerateToken (UserViewModelOutput userViewModelOutput);
    }
}
