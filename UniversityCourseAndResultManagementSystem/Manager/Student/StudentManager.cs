using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using UniversityCourseAndResultManagementSystem.Gateway.Student;
using UniversityCourseAndResultManagementSystem.Manager.Department;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Manager.Student
{
    public class StudentManager
    {
        StudentGateway studentGateway = new StudentGateway();
        DepartmentManager departmentManager = new DepartmentManager();
        public string SaveStudent(Models.Student student, string year)
        {
            int count = 0;
            List<Models.Student> students = GetAllStudents();
            string departmentCode = departmentManager.GetDeptCodeById(student.DepartmentId);
            foreach (var entry in students)
            {
                DateTime dateTime = Convert.ToDateTime(entry.Date);
                string entryYear = dateTime.Year.ToString();
                string entryDeptCode = departmentManager.GetDeptCodeById(entry.DepartmentId);
                if (entryYear==year && entryDeptCode == departmentCode)
                {
                    count += 1;
                }
            }
            if (count==0)
            {
                student.RegistrationNo = departmentCode + "-" + year + "-001";
            }
            else if(count>0 && count <10)
            {
                count = count + 1;
                student.RegistrationNo = departmentCode + "-" + year + "-00"+count;
            }
            else if (count>=10 && count<100)
            {
                count = count + 1;
                student.RegistrationNo = departmentCode + "-" + year + "-0" + count;
            }
            else
            {
                count = count + 1;
                student.RegistrationNo = departmentCode + "-" + year + "-" + count;
            }
            if (studentGateway.SaveStudent(student) > 0)
            {
                return "Student Saved Successfully";
            }
            return "Failed to save the student";
        }

        public List<Models.Student> GetAllStudents()
        {
            return studentGateway.GetAllStudents();
        }

        public Models.Student GetSavedStudent()
        {
            return studentGateway.SavedStudent;
        }

        public Models.StudentWithDepartmentName GetStudentByEmail(string email)
        {
            return studentGateway.GetStudentByEmail(email);
        }

        public List<StudentWithDepartmentName> GetAllStudentWithDepartmentNames()
        {
            return studentGateway.GetStudentWithDepartmentName();
        }

        public string EnrollCourse(EnrollCourse enrollCourse)
        {
            List<EnrolledCourseByStudent> enrolledCourseByStudents = GetAllEnrolledCourseByStudents();
            int count = 0;
            foreach (var course in enrolledCourseByStudents)
            {
                if (course.CourseId==enrollCourse.CourseId)
                {
                    if (course.RegNo==enrollCourse.RegNo)
                    {
                        count = 1;
                        break;
                    }
                }   
            }
            if (count==1)
            {
                return "Selected courses is already enrolled by this student.";
            }
            else
            {
                if (studentGateway.EnrollCourse(enrollCourse) > 0)
                {
                    return "Course Enrolled SuccessFully";
                }
                return "Course Enroll Failed.";
            }
        }

        public string GetStudentRegNoById(int id)
        {
            return studentGateway.GetRegNoById(id);
        }

        public List<EnrolledCourseByStudent> GetAllEnrolledCourseByStudents()
        {
            return studentGateway.GetAllEnrolledCourseByStudents();
        }
    }
}