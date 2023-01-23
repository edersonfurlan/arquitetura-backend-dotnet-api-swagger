using Courses.API.Business.Entities;
using Courses.API.Business.Repositories;
using Courses.API.Configurations;
using Courses.API.Filters;
using Courses.API.Models;
using Courses.API.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Courses.API.Controllers
{
    [Route("api/v1/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public UserController(ILogger<UserController> logger, 
                              IUserRepository userRepository, 
                              IAuthenticationService authenticationService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _authenticationService = authenticationService;
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
        public async Task<IActionResult> Logar(LoginViewModelInput loginViewModelInput)
        {
            try
            {
                User user = await _userRepository.GetUserAsync(loginViewModelInput.Login);

                if (user == null)
                    return BadRequest("Error during access attempt!");

                var userViewModelOutput = new UserViewModelOutput()
                {
                    Id = user.Id,
                    Login = loginViewModelInput.Login,
                    Email = user.Email
                };

                var token = _authenticationService.GenerateToken(userViewModelOutput);

                return Ok(new LoginViewModelOutput
                {
                    Token = token,
                    User = userViewModelOutput
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
            
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
            try
            {
                var user = await _userRepository.GetUserAsync(registerViewModelInput.Login);

                if (user != null)
                    return BadRequest("This user already exists!");

                user = new User
                {
                    Login = registerViewModelInput.Login,
                    Password = registerViewModelInput.Password,
                    Email = registerViewModelInput.Email
                };                

                _userRepository.Add(user);
                _userRepository.Commit();

                return Created("", registerViewModelInput);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }        
        }
    }
}
