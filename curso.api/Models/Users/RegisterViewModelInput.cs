using System.ComponentModel.DataAnnotations;

namespace curso.api.Models.Users
{
    public class RegisterViewModelInput
    {
        [Required(ErrorMessage = "The login is mandatory")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The e-mail is mandatory")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The password is mandatory")]
        public string Password { get; set; }
    }
}
