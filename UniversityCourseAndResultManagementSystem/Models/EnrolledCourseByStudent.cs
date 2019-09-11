using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class EnrolledCourseByStudent
    {
        public string RegNo { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int DepartmentId { get; set; }
        public string EnrollDate { get; set; }
        public int EnrollId { get; set; }
    }
}