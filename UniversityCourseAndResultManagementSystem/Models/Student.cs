using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter a name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter an email.")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email must be in valid format.")]
        [Remote("IsStudentExist", "Validation", ErrorMessage = "Email already exists.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Provide a Contact No.")]
        [DisplayName("Contact No")]
        [RegularExpression(@"^(?:\+?88)?01[15-9]\d{8}$", ErrorMessage = "Mobile No must be in correct format")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Please Select a Date.")]
        public string Date { get; set; }
        [Required(ErrorMessage = "Please enter an address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please Select a Department.")]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        public string RegistrationNo { get; set; }
    }
}