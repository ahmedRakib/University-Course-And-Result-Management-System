using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class StudentAndResult
    {
        public StudentWithDepartmentName Student { get; set; }
        public List<StudentResultDetails> StudentResultDetails { get; set; }
    }
}