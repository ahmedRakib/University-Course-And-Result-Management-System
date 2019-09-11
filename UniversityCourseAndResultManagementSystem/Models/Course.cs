using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Must have a code.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Code must be 5 characters long at least.")]
        [Remote("IsCourseCodeExist", "Validation", ErrorMessage = "Course Code already exists")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Must Enter a Name.")]
        [Remote("IsCourseNameExist", "Validation", ErrorMessage = "Course Name already exists")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Must have a credit.")]
        [Range(0.5, 5.0, ErrorMessage = "Credit must be between 0.5 to 5.0")]
        public double Credit { get; set; }
        [Required(ErrorMessage = "Must Provide a Description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Must select a Department.")]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Must select a Semester.")]
        [DisplayName("Semester")]
        public int SemesterId { get; set; }
    }
}