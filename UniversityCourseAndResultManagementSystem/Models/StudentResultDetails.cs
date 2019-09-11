using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class StudentResultDetails
    {
        public int StudentId { get; set; }
        public string RegistrationNo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string GradeLetter { get; set; }
    }
}