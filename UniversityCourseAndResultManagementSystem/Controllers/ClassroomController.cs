using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystem.Manager.Classroom;
using UniversityCourseAndResultManagementSystem.Manager.Course;
using UniversityCourseAndResultManagementSystem.Manager.Department;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class ClassroomController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        CourseManager courseManager = new CourseManager();
        ClassroomManager classroomManager = new ClassroomManager();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllocateClassrooms()
        {
            ViewBag.Departments = departmentManager.GetAllDepartments();
            ViewBag.Classrooms = classroomManager.GetAllClassrooms();
            ViewBag.Days = classroomManager.GetAllDays();
            return View();
        }
        [HttpPost]
        public ActionResult AllocateClassrooms(AllocateClassroom allocateClassroom)
        {
            DateTime fromTime = allocateClassroom.FromTime;
            if (fromTime.ToShortTimeString().Count() == 7)
            {
                allocateClassroom.StartingTime = 0 + fromTime.ToShortTimeString();
            }
            else
            {
                allocateClassroom.StartingTime = fromTime.ToShortTimeString();
            }
            DateTime toTime = allocateClassroom.ToTime;
            if (toTime.ToShortTimeString().Count() == 7)
            {
                allocateClassroom.EndingTime = 0 + toTime.ToShortTimeString();
            }
            else
            {
                allocateClassroom.EndingTime = toTime.ToShortTimeString();
            }

            ViewBag.Departments = departmentManager.GetAllDepartments();
            ViewBag.Classrooms = classroomManager.GetAllClassrooms();
            ViewBag.Days = classroomManager.GetAllDays();
            if (ModelState.IsValid)
            {
                ViewBag.Message = classroomManager.SaveClassroomAllocation(allocateClassroom);
            }
            return View();
        }

        public JsonResult GetCourseForDepartment(int departmentId)
        {
            var courses = courseManager.GetAllCourses();
            var selectedCourseList = courses.FindAll(c => c.DepartmentId == departmentId).ToList();

            return Json(selectedCourseList);
        }
        

        public ActionResult ViewClassAndRoomSchedule()
        {
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }

        public JsonResult GetClassAndRoomScheduleByDepartment(int departmentId)
        {
            var courses = courseManager.GetAllCourses();
            List<ClassScheduleInfo> schedule = classroomManager.GetAllClassScheduleInfo();
            List<ClassScheduleInfo> classSchedule2 = new List<ClassScheduleInfo>();
            foreach (var course in courses)
            {
                ClassScheduleInfo classSchedule = new ClassScheduleInfo();
                classSchedule.CourseId = course.Id;
                classSchedule.CourseCode = course.Code;
                classSchedule.CourseName = course.Name;
                classSchedule.DepartmentId = course.DepartmentId;
                foreach (var data in schedule)
                {
                    if (data.CourseId==course.Id)
                    {
                        classSchedule.ScheduleDetails += data.ScheduleDetails;
                    }
                }
                if (classSchedule.ScheduleDetails==null)
                {
                    classSchedule.ScheduleDetails = "Not Scheduled yet";
                }
                classSchedule2.Add(classSchedule);
            }

            List<ClassScheduleInfo> selectedDepartmentSchedule = classSchedule2.Where(d => d.DepartmentId == departmentId).ToList();
         
            return Json(selectedDepartmentSchedule);
        }

        public ActionResult UnallocateClassrooms()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnallocateClassrooms(Classroom classroom)
        {
            ViewBag.Message= classroomManager.UnallocateAllClassrooms();
            return View();
        }
    }
}