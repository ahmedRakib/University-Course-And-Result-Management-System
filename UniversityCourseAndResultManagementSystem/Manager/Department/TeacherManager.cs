using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Gateway.Department;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Manager.Department
{
    public class TeacherManager
    {
        private TeacherGateway teacherGateway = new TeacherGateway();

        public List<Designation> GetAllDesignations()
        {
            return teacherGateway.GetAllDesignations();
        }

        public string SaveTeacher(Teacher teacher)
        {
            if (teacherGateway.SaveTeacher(teacher) > 0)
            {
                return "Teacher Saved Successfully.";
            }
            return "Teacher Saving Failed.";
        }

        public List<Teacher> GetAllTeachers()
        {
            return teacherGateway.GetAllTeachers();
        }

        public string UpdateCreditStatus(int teacherId,double credit)
        {
            List<Teacher> teachers = teacherGateway.GetAllTeachers();
            double remaimingCredit = 0;
            foreach (var teacher in teachers)
            {
                if (teacher.Id==teacherId)
                {
                    remaimingCredit = teacher.RemainingCredit;
                    break;
                }
            }
            remaimingCredit -= credit;
            if (teacherGateway.UpdateCreditStatus(teacherId, remaimingCredit) > 0)
            {
                return "course Assigned to the teacher";
            }
            return "Course assign failed";
        }

        public void ResetCreditStatus()
        {
            teacherGateway.ResetCreditStatus();
        }

        public List<CourseWithAssignedTeacher> GetAllCourseWithAssignedTeachers()
        {
            return teacherGateway.CourseWithAssignedTeachers();
        }
    }
}