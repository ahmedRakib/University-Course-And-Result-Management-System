using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class SelectedStudentAndCoursesList
    {
        public StudentWithDepartmentName Student { get; set; }
        public List<EnrolledCourseByStudent> Courses { get; set; }
    }
}