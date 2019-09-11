using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.BuilderProperties;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Must Provide a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Must provide an address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Must provide a valid email")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email must be in valid format.")]
        [Remote("IsTeacherExist", "Validation",ErrorMessage = "Email Already Exists")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Must enter a contact number")]
        [DisplayName("Contact No")]
        [RegularExpression(@"^(?:\+?88)?01[15-9]\d{8}$", ErrorMessage = "Mobile Number must be in correct format")]
        public string ContactNo { get; set; }
        [DisplayName("Designation")]
        public int DesignationId { get; set; }
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Must enter allotted credit")]
        [DataType("number", ErrorMessage = "Credit must be a number")]
        [Range(0, int.MaxValue, ErrorMessage = "Credit cannot be a negative number.")]
        [DisplayName("Credit to be taken")]
        public double AllottedCredit { get; set; }
        public double RemainingCredit { get; set; }
    }
}