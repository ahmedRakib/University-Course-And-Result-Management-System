using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class StudentAndCourses
    {
        public StudentWithDepartmentName Student { get; set; }
        public List<Course> Courses { get; set; }
    }
}