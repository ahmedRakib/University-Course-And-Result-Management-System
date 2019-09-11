using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using UniversityCourseAndResultManagementSystem.Manager.Department;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        TeacherManager teacherManager = new TeacherManager();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(Department department)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = departmentManager.SaveDepartment(department);
                return RedirectToAction("ViewAllDepartment");
            }
            return View();
        }

        public ActionResult ViewAllDepartment()
        {
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }

        public ActionResult SaveTeacher()
        {
            ViewBag.Designations = teacherManager.GetAllDesignations();
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }
        [HttpPost]
        public ActionResult SaveTeacher(Teacher teacher)
        {
            ViewBag.Designations = teacherManager.GetAllDesignations();
            ViewBag.Departments = departmentManager.GetAllDepartments();
            if (ModelState.IsValid)
            {
                ViewBag.Message = teacherManager.SaveTeacher(teacher);
                return View();
            }
            return View();
        }
	}
}