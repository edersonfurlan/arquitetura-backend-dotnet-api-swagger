using System.ComponentModel.DataAnnotations;

namespace Courses.API.Models.Users
{
    public class LoginViewModelInput
    {
        [Required(ErrorMessage = "The login is mandatory")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The password is mandatory")]
        public string Password { get; set; }
    }
}
