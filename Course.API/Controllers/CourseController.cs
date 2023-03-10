using Courses.API.Business.Entities;
using Courses.API.Business.Repositories;
using Courses.API.Models.Courses;
using Courses.API.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace Courses.API.Controllers
{
    [Route("api/v1/course")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<CourseController> _logger;


        public CourseController(ICourseRepository courseRepository, ILogger<CourseController> logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }

        [SwaggerResponse(statusCode: 201, description: "Success in registering a course", Type = typeof(CourseViewModelOutput))]
        [SwaggerResponse(statusCode: 401, description: "Not authorized")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CourseViewModelInput courseViewModelInput)
        {
            try
            {
                Course course = new Course()
                {
                    Name = courseViewModelInput.Name,
                    Description = courseViewModelInput.Description
                };                
                var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
                course.UserId = userId;

                _courseRepository.Add(course);
                _courseRepository.Commit();

                var courseViewModelOutput = new CourseViewModelOutput()
                {
                    Name = course.Name,
                    Description= course.Description
                };

                return Created("", courseViewModelOutput);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }

            
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
            try
            {
                var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

                var courses = _courseRepository.GetByUserId(userId)
                                .Select(s => new CourseViewModelOutput()
                                {
                                    Name = s.Name,
                                    Description = s.Description
                                });

                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }           
        }
    }
}
