using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Models.Courses;
using curso.api.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace curso.api.Controllers
{
    [Route("api/v1/course")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [SwaggerResponse(statusCode: 201, description: "Success in registering a course", Type = typeof(CourseViewModelOutput))]
        [SwaggerResponse(statusCode: 401, description: "Not authorized")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CourseViewModelInput courseViewModelInput)
        {
            Course course = new Course();
            course.Name = courseViewModelInput.Name;
            course.Description = courseViewModelInput.Description;
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            course.UserId = userId;

            _courseRepository.Add(course);
            _courseRepository.Commit();

            return Created("", courseViewModelInput);
        }

        /// <summary>
        /// Este serviço permite obter todos os cursos ativos do usuário.
        /// </summary>
        /// <returns>Retorna status ok e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 200, description: "Success in obtaining a course", Type = typeof(CourseViewModelOutput))]
        [SwaggerResponse(statusCode: 401, description: "Not authorized")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var courses = _courseRepository.GetByUserId(userId)
                            .Select(s => new CourseViewModelOutput()
                            {
                                Name = s.Name,
                                Description = s.Description,
                                Login = s.User.Login
                            });

            return Ok(courses);
        }
    }
}
