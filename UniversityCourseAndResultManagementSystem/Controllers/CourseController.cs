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
    public class CourseController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        CourseManager courseManager = new CourseManager();
        TeacherManager teacherManager = new TeacherManager();
        ClassroomManager classroomManager = new ClassroomManager();

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult SaveCouse()
        {
            ViewBag.Departments = departmentManager.GetAllDepartments();
            ViewBag.Semesters = courseManager.GetAllSemesters();
            return View();
        }
        [HttpPost]
        public ActionResult SaveCouse(Course course)
        {
            ViewBag.Departments = departmentManager.GetAllDepartments();
            ViewBag.Semesters = courseManager.GetAllSemesters();
            if (ModelState.IsValid)
            {
                ViewBag.Message = courseManager.SaveCourse(course);
                return View();
            }
            return View(course);
        }

        public ActionResult AssignCourse()
        {
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }
        [HttpPost]
        public ActionResult AssignCourse(int TeacherId, int CourseId, double CourseCredit)
        {
            ViewBag.Departments = departmentManager.GetAllDepartments();
            if (Math.Ceiling(CourseCredit) != 0.00)
            {
                ViewBag.Message = courseManager.AssignCourse(TeacherId, CourseId);
                if (ViewBag.Message=="Course Assigned Successfully")
                {
                    teacherManager.UpdateCreditStatus(TeacherId, CourseCredit);
                }
                return View();
            }
            return View();
        }

        public JsonResult GetTeacherByDepartment(int Id)
        {
            var teachers = teacherManager.GetAllTeachers();
            var teachersList = teachers.Where(t => t.DepartmentId == Id).ToList();

            return Json(teachersList);
        }
        public JsonResult GetCourseByDepartment(int Id)
        {
            var courses = courseManager.GetAllCourses();
            var courseList = courses.Where(c => c.DepartmentId == Id).ToList();

            return Json(courseList);
        }
        public JsonResult GetCreditByTeacher(int Id)
        {
            var teacher = teacherManager.GetAllTeachers();
            var teacherList = teacher.Find(t => t.Id == Id);

            return Json(teacherList);
        }

        public JsonResult GetCourseDetails(int Id)
        {
            var courses = courseManager.GetAllCourses();
            var selectdCourseDetails = courses.Find(c => c.Id == Id);

            return Json(selectdCourseDetails);
        }

        public ActionResult ViewCourse()
        {
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }

        public JsonResult GetCourseStatics(int departmentId)
        {
            var courseDetails = courseManager.GetAllCourseWithSemester();
            var courseWithAssignedTeachersData = teacherManager.GetAllCourseWithAssignedTeachers();
            List<CourseDetailsWithTDS> courseData = new List<CourseDetailsWithTDS>();
            foreach (var course in courseDetails)
            {
                if (course.DepatmentId==departmentId)
                {
                    CourseDetailsWithTDS courseDetailsWithTds = new CourseDetailsWithTDS();
                    courseDetailsWithTds.CourseCode = course.CourseCode;
                    courseDetailsWithTds.CourseName = course.CourseName;
                    courseDetailsWithTds.Semester = course.Semester;
                    courseDetailsWithTds.TeacherName = "Not assigned yet";
                    foreach (var data in courseWithAssignedTeachersData)
                    {
                        if (course.CourseId==data.CourseId)
                        {
                            courseDetailsWithTds.TeacherName = data.TeacherName;
                            break;
                        }
                        else
                        {
                            courseDetailsWithTds.TeacherName = "Not assigned yet";
                        }
                        
                    }
                    
                    courseData.Add(courseDetailsWithTds);
                }
            }

            return Json(courseData);
        }

        public ActionResult UnassignAllCourses()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnassignAllCourses(Course course)
        {
            ViewBag.Message = courseManager.UnassignAllCourses();
            if (ViewBag.Message=="All Courses Unassigned")
            {
                teacherManager.ResetCreditStatus();
            }
            return View();
        }
    }
}