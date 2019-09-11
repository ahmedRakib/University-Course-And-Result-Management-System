using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway.Student
{
    public class StudentGateway : Gateway
    {
        public Models.Student SavedStudent { get; set; }
        public int SaveStudent(Models.Student student)
        {
            SavedStudent = student;
            Qurey = "INSERT INTO student VALUES(@name, @registrationNo, @email, @contactNo, @date, @address, @departmentId)";
            Command = new SqlCommand(Qurey, Connection);
            Command.Parameters.AddWithValue("@name", student.Name);
            Command.Parameters.AddWithValue("@registrationNo", student.RegistrationNo);
            Command.Parameters.AddWithValue("@email", student.Email);
            Command.Parameters.AddWithValue("@contactNo", student.ContactNo);
            Command.Parameters.AddWithValue("@date", student.Date);
            Command.Parameters.AddWithValue("@address", student.Address);
            Command.Parameters.AddWithValue("@departmentId", student.DepartmentId);
            Connection.Open();

            RowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return RowAffected;
        }

        public List<Models.Student> GetAllStudents()
        {
            Qurey = "SELECT * FROM student";
            Command = new SqlCommand(Qurey, Connection);
            List<Models.Student> students = new List<Models.Student>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Models.Student student = new Models.Student();
                student.Id = Convert.ToInt32(Reader["id"]);
                student.Name = Reader["name"].ToString();
                student.RegistrationNo = Reader["registrationNo"].ToString();
                student.Email = Reader["email"].ToString();
                student.ContactNo = Reader["contactNo"].ToString();
                student.Address = Reader["address"].ToString();
                DateTime dateTime = Convert.ToDateTime(Reader["date"]);
                student.Date = dateTime.ToShortDateString();
                student.DepartmentId = Convert.ToInt32(Reader["departmentId"]);

                students.Add(student);
            }
            Reader.Close();
            Connection.Close();

            return students;
        }

        public Models.StudentWithDepartmentName GetStudentByEmail(string email)
        {
            Qurey = "SELECT * FROM studentWithDepartmentName WHERE email = @email";
            Command = new SqlCommand(Qurey, Connection);
            Command.Parameters.AddWithValue("@email", email);
            Models.StudentWithDepartmentName student = null;

            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                student = new Models.StudentWithDepartmentName();
                student.Name = Reader["name"].ToString();
                student.RegNo = Reader["registrationNo"].ToString();
                student.Email = Reader["email"].ToString();
                student.Contact = Reader["contactNo"].ToString();
                student.Address = Reader["address"].ToString();
                DateTime dateTime = Convert.ToDateTime(Reader["date"]);
                student.Date = dateTime.ToShortDateString();
                student.DepartmentName = Reader["departmentName"].ToString();
            }
            Reader.Close();
            Connection.Close();

            return student;
        }
        public List<Models.StudentWithDepartmentName> GetStudentWithDepartmentName()
        {
            Qurey = "SELECT * FROM studentWithDepartmentName";
            Command = new SqlCommand(Qurey, Connection);
            List<StudentWithDepartmentName> studentsWithDepartment = new List<StudentWithDepartmentName>();

            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                StudentWithDepartmentName student = new Models.StudentWithDepartmentName();
                student.StudentId = Convert.ToInt32(Reader["id"]);
                student.Name = Reader["name"].ToString();
                student.RegNo = Reader["registrationNo"].ToString();
                student.Email = Reader["email"].ToString();
                student.Contact = Reader["contactNo"].ToString();
                student.Address = Reader["address"].ToString();
                DateTime dateTime = Convert.ToDateTime(Reader["date"]);
                student.Date = dateTime.ToShortDateString();
                student.DepartmentId = Convert.ToInt32(Reader["departmentId"]);
                student.DepartmentName = Reader["departmentName"].ToString();
                student.DepartmentCode = Reader["code"].ToString();

                studentsWithDepartment.Add(student);
            }
            Reader.Close();
            Connection.Close();

            return studentsWithDepartment;
        }

        public int EnrollCourse(EnrollCourse enrollCourse)
        {
            Qurey = "INSERT INTO enrollCourse VALUES(@regNo, @courseId, @enrollDate)";
            Command = new SqlCommand(Qurey, Connection);
            Command.Parameters.AddWithValue("@regNo", enrollCourse.RegNo);
            Command.Parameters.AddWithValue("@courseId", enrollCourse.CourseId);
            Command.Parameters.AddWithValue("@enrollDate", enrollCourse.Date);

            Connection.Open();
            RowAffected = Command.ExecuteNonQuery();
            Connection.Close();

            return RowAffected;
        }

        public string GetRegNoById(int id)
        {
            Qurey = "SELECT * FROM student where id=@id";
            Command = new SqlCommand(Qurey, Connection);
            Command.Parameters.AddWithValue("@id", id);
            string regNo = "";
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.HasRows)
            {
                Reader.Read();
                regNo = Reader["registrationNo"].ToString();
            }
            Reader.Close();
            Connection.Close();

            return regNo;
        }

        public List<EnrolledCourseByStudent> GetAllEnrolledCourseByStudents()
        {
            Qurey = "SELECT * FROM enrolledCourseByStudent";
            Command = new SqlCommand(Qurey, Connection);
            List<EnrolledCourseByStudent> enrolledCourseByStudents = new List<EnrolledCourseByStudent>();

            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                EnrolledCourseByStudent enrolledCourse = new EnrolledCourseByStudent();
                enrolledCourse.CourseId = Convert.ToInt32(Reader["courseId"]);
                enrolledCourse.CourseName = Reader["courseName"].ToString();
                enrolledCourse.CourseCode = Reader["courseCode"].ToString();
                enrolledCourse.DepartmentId = Convert.ToInt32(Reader["departmentId"]);
                if (Reader["enrollDate"]==DBNull.Value)
                {
                    enrolledCourse.EnrollDate = "N/A";
                }
                else
                {
                    enrolledCourse.EnrollDate = Reader["enrollDate"].ToString();
                }
                if (Reader["enrollId"]==DBNull.Value)
                {
                    enrolledCourse.EnrollId = 0;
                }
                else
                {
                    enrolledCourse.EnrollId = Convert.ToInt32(Reader["enrollId"]);
                }
                if (Reader["registrationNo"]==DBNull.Value)
                {
                    enrolledCourse.RegNo = "N/A";
                }
                else
                {
                    enrolledCourse.RegNo = Reader["registrationNo"].ToString();
                }
                if (Reader["studentId"]==DBNull.Value)
                {
                    enrolledCourse.StudentId = 0;
                }
                else
                {
                    enrolledCourse.StudentId = Convert.ToInt32(Reader["studentId"]);
                }

                enrolledCourseByStudents.Add(enrolledCourse);
            }
            Reader.Close();
            Connection.Close();
            return enrolledCourseByStudents;
        } 
    }
}