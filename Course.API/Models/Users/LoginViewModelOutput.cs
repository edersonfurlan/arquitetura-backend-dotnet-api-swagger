namespace Courses.API.Models.Users
{
    public class LoginViewModelOutput
    {
        public string Token { get; set; }

        public UserViewModelOutput User { get; set; }
    }
}
