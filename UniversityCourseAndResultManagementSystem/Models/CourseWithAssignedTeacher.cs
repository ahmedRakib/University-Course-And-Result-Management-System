using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class CourseWithAssignedTeacher
    {
        public int AssignId { get; set; }
        public int CourseId { get; set; }
        public string Status { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Semester { get; set; }
    }
}