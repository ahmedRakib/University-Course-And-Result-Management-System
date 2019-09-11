using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class CourseDetailsWithTDS
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int CourseId { get; set; }
        public string TeacherName { get; set; }
        public int TeacherId { get; set; }
        public string Semester { get; set; }
        public int DepatmentId { get; set; }
    }
}