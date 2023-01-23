﻿using System.ComponentModel.DataAnnotations;

namespace curso.api.Models.Courses
{
    public class CourseViewModelInput
    {
        [Required(ErrorMessage = "The name is mandatory")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The description is mandatory")]
        public string Description { get; set; }
    }
}
