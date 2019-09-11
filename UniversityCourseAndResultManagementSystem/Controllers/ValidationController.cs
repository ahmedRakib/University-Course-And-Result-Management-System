using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystem.Manager.Course;
using UniversityCourseAndResultManagementSystem.Manager.Department;
using UniversityCourseAndResultManagementSystem.Manager.Student;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class ValidationController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        TeacherManager teacherManager = new TeacherManager();
        StudentManager studentManager = new StudentManager();
        CourseManager courseManager = new CourseManager();
        public JsonResult IsDepartmentCodeExist(string Code)
        {
            bool isExist = departmentManager.GetAllDepartments().Where(u => u.Code.ToLowerInvariant().Equals(Code.ToLower())).FirstOrDefault() == null;
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsDepartmentNameExist(string Code, string DepartmentName)
        {
            bool isExist = departmentManager.GetAllDepartments().Where(u => u.DepartmentName.ToLowerInvariant().Equals(DepartmentName.ToLower())).FirstOrDefault() == null;
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsCourseCodeExist(string Code)
        {
            bool isExist = courseManager.GetAllCourses().Where(u => u.Code.ToLowerInvariant().Equals(Code.ToLower())).FirstOrDefault() == null;
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsCourseNameExist(string Code, string Name)
        {
            bool isExist = courseManager.GetAllCourses().Where(u => u.Name.ToLowerInvariant().Equals(Name.ToLower())).FirstOrDefault() == null;
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsTeacherExist(string Email)
        {
            bool isExist = teacherManager.GetAllTeachers().Where(u => u.Email.ToLowerInvariant().Equals(Email.ToLower())).FirstOrDefault() == null;
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsStudentExist(string Email)
        {
            List<Student> students = studentManager.GetAllStudents();
            bool isExist = students.Where(s => s.Email.ToLowerInvariant().Equals(Email.ToLower())).FirstOrDefault() == null;
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
	}
}