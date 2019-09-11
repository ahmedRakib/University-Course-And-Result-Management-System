using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Must enter a code.")]
        [StringLength(7, MinimumLength = 2, ErrorMessage = "Code must be between 2-7 character.")]
        [Remote("IsDepartmentCodeExist", "Validation", ErrorMessage = "Department Already Exist.")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Must enter a name.")]
        [DisplayName("Name")]
        [Remote("IsDepartmentNameExist", "Validation", ErrorMessage = "Department Aleady Exists.")]
        public string DepartmentName { get; set; }
    }
}