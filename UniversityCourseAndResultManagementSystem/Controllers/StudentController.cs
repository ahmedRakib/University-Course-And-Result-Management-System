using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using UniversityCourseAndResultManagementSystem.Manager.Course;
using UniversityCourseAndResultManagementSystem.Manager.Department;
using UniversityCourseAndResultManagementSystem.Manager.Student;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        StudentManager studentManager = new StudentManager();
        CourseManager courseManager = new CourseManager();
        StudentResultManager studentResultManager = new StudentResultManager();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegisterStudent()
        {
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }
        [HttpPost]
        public ActionResult RegisterStudent(Student student)
        {
            TempData["email"] = student.Email;
            DateTime date = Convert.ToDateTime(student.Date);
            string year = date.Year.ToString();
            ViewBag.Departments = departmentManager.GetAllDepartments();
            if (ModelState.IsValid)
            {
                ViewBag.Message = studentManager.SaveStudent(student, year);
                if (ViewBag.Message == "Student Saved Successfully")
                {
                    return RedirectToAction("RegisteredStudentDetails", "Student");
                }
            }
            return View();
        }

        public ActionResult RegisteredStudentDetails()
        {
            string email = TempData["email"].ToString();
            ViewBag.Student = studentManager.GetStudentByEmail(email);
            return View();
        }

        public ActionResult EnrollInACourse()
        {
            ViewBag.Students = studentManager.GetAllStudents();
            return View();
        }
        [HttpPost]
        public ActionResult EnrollInACourse(EnrollCourse enrollCourse)
        {
            int id = Convert.ToInt32(enrollCourse.RegNo);
            enrollCourse.RegNo = studentManager.GetStudentRegNoById(id);
            ViewBag.Students = studentManager.GetAllStudents();
            if (ModelState.IsValid)
            {
                ViewBag.Message = studentManager.EnrollCourse(enrollCourse);
            }
            return View();
        }

        public JsonResult GetStudentDetails(int studentId)
        {
            var studentdata = studentManager.GetAllStudentWithDepartmentNames();
            var selectedStudent = studentdata.Find(s => s.StudentId == studentId);
            var coursesData = courseManager.GetAllCourses();
            var coursesOfSelectedStudent = coursesData.Where(c => c.DepartmentId == selectedStudent.DepartmentId).ToList();

            StudentAndCourses studentAndCourses = new StudentAndCourses();
            studentAndCourses.Student = selectedStudent;
            studentAndCourses.Courses = coursesOfSelectedStudent;
            return Json(studentAndCourses);
        }

        public ActionResult SaveStudentResult()
        {
            ViewBag.students = studentManager.GetAllStudents();
            ViewBag.grades = studentResultManager.GetAllResultGrades();
            return View();
        }
        [HttpPost]
        public ActionResult SaveStudentResult(SaveStudentResult saveStudentResult)
        {
            saveStudentResult.RegNo = studentManager.GetStudentRegNoById(saveStudentResult.StudentId);
            ViewBag.students = studentManager.GetAllStudents();
            ViewBag.grades = studentResultManager.GetAllResultGrades();
            if (ModelState.IsValid)
            {
                ViewBag.Message = studentResultManager.SaveStudentResultData(saveStudentResult);
            }

            return View();
        }

        public JsonResult GetStudentDataById(int studentId)
        {
            var studentdata = studentManager.GetAllStudentWithDepartmentNames();
            var selectedStudent = studentdata.Find(s => s.StudentId == studentId);
            var coursesData = studentManager.GetAllEnrolledCourseByStudents();
            var coursesOfSelectedStudent = coursesData.Where(c => c.StudentId == studentId).ToList();

            SelectedStudentAndCoursesList selectedStudentAndCourses = new SelectedStudentAndCoursesList();
            selectedStudentAndCourses.Student = selectedStudent;
            selectedStudentAndCourses.Courses = coursesOfSelectedStudent;
            return Json(selectedStudentAndCourses);
        }

        public ActionResult ViewResult()
        {
            ViewBag.Students = studentManager.GetAllStudents();
            return View();
        }
        [HttpPost]
        public ActionResult ViewResult(Student student)
        {
            return RedirectToAction("GeneratePdf", "Student");
        }

        public JsonResult GetStudentAndResult(int studentId)
        {
            var student = studentManager.GetAllStudentWithDepartmentNames();
            var studentResultDetails = studentResultManager.GetAllStudentResultDetails();

            var selectedStudent = student.Find(s => s.StudentId == studentId);
            var selectedStudentResultDetails = studentResultDetails.Where(s => s.StudentId == studentId).ToList();
            TempData["Id"] = studentId;
            StudentAndResult studentAndResult = new StudentAndResult();
            studentAndResult.Student = selectedStudent;
            studentAndResult.StudentResultDetails = selectedStudentResultDetails;

            return Json(studentAndResult);
        }

        public ActionResult GeneratePdf(Student student)
        {
            int studentId = (int) TempData["id"];
            var results = studentResultManager.GetAllStudentResultDetails();
            var students = studentManager.GetAllStudentWithDepartmentNames();

            var selectedStudent = students.Find(s => s.StudentId == studentId);
            var selectedStudentResult = results.Where(r => r.StudentId == studentId).ToList();

            ViewBag.Student = selectedStudent;
            ViewBag.Result = selectedStudentResult;
            return new ViewAsPdf("GeneratePdf", "Student")
            {
                FileName = "Result sheet of "+selectedStudent.RegNo+".pdf"
            };
        }
    }
}