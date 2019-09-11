using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway.Course
{
    public class CourseGateway:Gateway
    {
        public List<Semester> GetAllSemesters()
        {
            Qurey = "SELECT * FROM semester";
            Command = new SqlCommand(Qurey, Connection);
            List<Semester> semesters = new List<Semester>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Semester semester = new Semester();
                semester.id = Convert.ToInt32(Reader["id"]);
                semester.SemesterName = Reader["semester"].ToString();

                semesters.Add(semester);
            }
            Reader.Close();
            Connection.Close();
            return semesters;
        }

        public int SaveCourse(Models.Course course)
        {
            Qurey =
                "INSERT INTO course VALUES(@courseCode, @courseName, @credit, @description, @departmentId, @semesterId)";
            Command = new SqlCommand(Qurey, Connection);
            Command.Parameters.AddWithValue("@courseCode", course.Code);
            Command.Parameters.AddWithValue("@courseName", course.Name);
            Command.Parameters.AddWithValue("@credit", course.Credit);
            Command.Parameters.AddWithValue("@description", course.Description);
            Command.Parameters.AddWithValue("@departmentId", course.DepartmentId);
            Command.Parameters.AddWithValue("@semesterId", course.SemesterId);

            Connection.Open();
             RowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return RowAffected;
        }

        public List<Models.Course> GetAllCourses()
        {
            Qurey = "SELECT * FROM course";
            Command = new SqlCommand(Qurey, Connection);
            List<Models.Course> courses = new List<Models.Course>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Models.Course course = new Models.Course();
                course.Id = Convert.ToInt32(Reader["id"]);
                course.Code = Reader["courseCode"].ToString();
                course.Name = Reader["courseName"].ToString();
                course.Credit = Convert.ToDouble(Reader["credit"]);
                course.DepartmentId = Convert.ToInt32(Reader["departmentId"]);
                course.Description = Reader["description"].ToString();
                course.SemesterId = Convert.ToInt32(Reader["semesterId"]);

                courses.Add(course);
            }
            Reader.Close();
            Connection.Close();
            return courses;
        }

        public int AssignCourse(int teacherId, int courseId)
        {
            Qurey = "INSERT INTO assignCourses VALUES(@courseId, @teacherId, @status)";
            Command = new SqlCommand(Qurey, Connection);
            Command.Parameters.AddWithValue("@teacherId", teacherId);
            Command.Parameters.AddWithValue("@courseId", courseId);
            Command.Parameters.AddWithValue("@status", "active");

            Connection.Open();
            RowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return RowAffected;
        }

        public List<CourseDetailsWithTDS> GetCourseDetails()
        {
            Qurey = "SELECT * FROM courseDetailsWithTeacherSemesterDepartment";
            Command = new SqlCommand(Qurey, Connection);
            List<CourseDetailsWithTDS> coursesdata = new List<CourseDetailsWithTDS>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                CourseDetailsWithTDS courseDetails = new CourseDetailsWithTDS();
                courseDetails.CourseCode = Reader["courseCode"].ToString();
                courseDetails.CourseName = Reader["courseName"].ToString();
                courseDetails.CourseId = Convert.ToInt32(Reader["courseId"]);
                if (Reader["teacherName"]==DBNull.Value)
                {
                    courseDetails.TeacherName = "Not Assigned Yet";
                }
                else
                {
                    courseDetails.TeacherName = Reader["teacherName"].ToString();
                }
                if (Reader["teacherId"]==DBNull.Value)
                {
                    courseDetails.TeacherId = 0;
                }
                else
                {
                    courseDetails.TeacherId = Convert.ToInt32(Reader["teacherId"]);
                }
                courseDetails.Semester = Reader["semester"].ToString();
                courseDetails.DepatmentId = Convert.ToInt32(Reader["departmentId"]);

                coursesdata.Add(courseDetails);
            }
            Reader.Close();
            Connection.Close();
            return coursesdata;
        }

        public List<CourseDetailsWithTDS> GetAllAssignedCourses()
        {
            Qurey = "SELECT * FROM AssignCourses WHERE status=@status";
            Command = new SqlCommand(Qurey,Connection);
            Command.Parameters.AddWithValue("@status", "active");
            List<CourseDetailsWithTDS> courseDetails = new List<CourseDetailsWithTDS>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                CourseDetailsWithTDS course = new CourseDetailsWithTDS();
                course.CourseId = Convert.ToInt32(Reader["courseId"]);
                course.TeacherId = Convert.ToInt32(Reader["teacherId"]);

                courseDetails.Add(course);
            }
            Reader.Close();
            Connection.Close();
            return courseDetails;
        }

        public List<CourseWithAllocationId> GetAllCourseWithAllocationId()
        {
            Qurey = "SELECT * FROM courseWithAllocation";
            Command = new SqlCommand(Qurey, Connection);
            List<CourseWithAllocationId> coursesWithAllocationId = new List<CourseWithAllocationId>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                CourseWithAllocationId courseWithAllocation = new CourseWithAllocationId();
                courseWithAllocation.CourseId = Convert.ToInt32(Reader["id"]);
                courseWithAllocation.CourseCode = Reader["courseCode"].ToString();
                courseWithAllocation.CourseName = Reader["courseName"].ToString();
                if (Reader["allocationId"]==DBNull.Value)
                {
                    courseWithAllocation.AllocationId = 0;
                }
                else
                {
                    courseWithAllocation.AllocationId = Convert.ToInt32(Reader["allocationId"]);
                }
                courseWithAllocation.DeparmentId = Convert.ToInt32(Reader["departmentId"]);
                coursesWithAllocationId.Add(courseWithAllocation);
            }
            Reader.Close();
            Connection.Close();
            return coursesWithAllocationId;
        }

        public int UnassignAllCourses()
        {
            Qurey = "UPDATE assignCourses SET status=@status";
            Command = new SqlCommand(Qurey, Connection);
            Command.Parameters.AddWithValue("@status", "inactive");

            Connection.Open();
            RowAffected = Command.ExecuteNonQuery();
            Connection.Close();

            return RowAffected;
        }

         public List<CourseDetailsWithTDS> GetAllCourseWithSemester()
        {
            Qurey = "SELECT * FROM courseWithSemester";
            Command = new SqlCommand(Qurey,Connection);
            List<CourseDetailsWithTDS> courseDetails = new List<CourseDetailsWithTDS>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                CourseDetailsWithTDS course = new CourseDetailsWithTDS();
                course.CourseId = Convert.ToInt32(Reader["id"]);
                course.CourseCode = Reader["courseCode"].ToString();
                course.CourseName = Reader["courseName"].ToString();
                course.Semester = Reader["semester"].ToString();
                course.DepatmentId = Convert.ToInt32(Reader["departmentId"]);


                courseDetails.Add(course);
            }
            Reader.Close();
            Connection.Close();
            return courseDetails;
        }
    }
}