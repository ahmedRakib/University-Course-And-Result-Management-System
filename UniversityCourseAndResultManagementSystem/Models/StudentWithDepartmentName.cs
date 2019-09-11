using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class StudentWithDepartmentName
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string RegNo { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Date { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
    }
}