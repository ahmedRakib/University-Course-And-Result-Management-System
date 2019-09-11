using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class SaveStudentResult
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string RegNo { get; set; }
        public int CourseId { get; set; }
        public int GradeId { get; set; }
    }
}