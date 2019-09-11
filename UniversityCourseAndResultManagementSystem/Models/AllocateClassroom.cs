using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class AllocateClassroom
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Select a Department")]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please Select a Course")]
        [DisplayName("Course")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Please Select a Room")]
        [DisplayName("Room")]
        public int RoomId { get; set; }
        [Required(ErrorMessage = "Please Select a Day")]
        [DisplayName("Day")]
        public int DayId { get; set; }
        [Required(ErrorMessage = "Please Select a Starting Time")]
        [DisplayName("From")]
        public DateTime FromTime { get; set; }
        [Required(ErrorMessage = "Please Select a Ending Time")]
        [DisplayName("To")]
        public DateTime ToTime { get; set; }

        public string StartingTime { get; set; }
        public string EndingTime { get; set; }
    }
}