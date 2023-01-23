using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Configurations;
using curso.api.Filters;
using curso.api.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace curso.api.Controllers
{
    [Route("api/v1/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public UserController(IUserRepository userRepository, 
                              IConfiguration configuration, 
                              IAuthenticationService authentication)
        {
            _userRepository = userRepository;
            _authenticationService = authentication;
        }

        /// <summary>
        /// Este serviço permite autenticar um usuário cadastrado e ativo.
        /// </summary>
        /// <param name="loginViewModelInput">View model do login</param>
        [SwaggerResponse(statusCode: 200, description: "Success in authentication", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "The fields are mandatory", Type = typeof(FieldValidationViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Internal error", Type = typeof(GenericErrorViewModel))]
        [HttpPost]
        [Route("login")]
        [CustomModelStateValidation]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            User user = _userRepository.GetUser(loginViewModelInput.Login);

            if (user == null)
                return BadRequest("Error during access attempt!");

            //if(user.Password != loginViewModelInput.Password.GetEncryptedPassword())
            //    return BadRequest("Error during access attempt! Chekc your password.");

            var userViewModelOutput = new UserViewModelOutput()
            {
                Id = user.Id,
                Login = loginViewModelInput.Login,
                Email = user.Email
            };

            var token = _authenticationService.GenerateToken(userViewModelOutput);

            return Ok(new 
            { 
                Token = token,
                User = userViewModelOutput
            });
        }

        /// <summary>
        /// Este serviço permite cadastrar um usuário cadastrado não existente.
        /// </summary>
        /// <param name="registerViewModelInput">View model do registro de login</param>
        [SwaggerResponse(statusCode: 200, description: "Successfully registered", Type = typeof(RegisterViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "The fields are mandatory", Type = typeof(FieldValidationViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Internal error", Type = typeof(GenericErrorViewModel))]
        [HttpPost]
        [Route("register")]
        [CustomModelStateValidation]
        public async Task<IActionResult> Register(RegisterViewModelInput registerViewModelInput)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<CourseDBContext>();
            //optionsBuilder.UseSqlServer("Data Source=BRPC013729\\SQLEXPRESS;Initial Catalog=Course;Integrated Security=True;Trust Server Certificate=true");
            //CourseDBContext context = new CourseDBContext(optionsBuilder.Options);

            //var pendingMigrations = context.Database.GetPendingMigrations();
            //if (pendingMigrations.Count() > 0)
            //    context.Database.Migrate();

            var user = new User();
            user.Login = registerViewModelInput.Login;
            user.Password = registerViewModelInput.Password;
            user.Email = registerViewModelInput.Email;

            _userRepository.Add(user);
            _userRepository.Commit();

            return Created("", registerViewModelInput);
        }
    }
}
