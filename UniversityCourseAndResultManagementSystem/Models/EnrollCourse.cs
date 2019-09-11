using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class EnrollCourse
    {
        public int Id { get; set; }
        public string RegNo { get; set; }
        public int CourseId { get; set; }
        public string Date { get; set; }
    }
}