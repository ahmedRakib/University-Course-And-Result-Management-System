using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway.Student
{
    public class StudentResultGateway:Gateway
    {
        public List<ResultGrade> GetAllResultGrades()
        {
            Qurey = "SELECT * FROM resultGrades";
            Command = new SqlCommand(Qurey, Connection);
            List<ResultGrade> resultGrades = new List<ResultGrade>();
            
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                ResultGrade resultGrade = new ResultGrade();
                resultGrade.Id = Convert.ToInt32(Reader["id"]);
                resultGrade.GradeLetter = Reader["gradeLetter"].ToString();

                resultGrades.Add(resultGrade);
            }
            Reader.Close();
            Connection.Close();

            return resultGrades;
        }

        public int SaveStudentResultData(SaveStudentResult saveStudentResult)
        {
            Qurey = "INSERT INTO studentResult VALUES(@studentId, @regNo, @courseId, @gradeId)";
            Command = new SqlCommand(Qurey, Connection);
            Command.Parameters.AddWithValue("@studentId", saveStudentResult.StudentId);
            Command.Parameters.AddWithValue("@regNo", saveStudentResult.RegNo);
            Command.Parameters.AddWithValue("@courseId", saveStudentResult.CourseId);
            Command.Parameters.AddWithValue("@gradeId", saveStudentResult.GradeId);

            Connection.Open();
            RowAffected = Command.ExecuteNonQuery();
            Connection.Close();

            return RowAffected;
        }
        public List<SaveStudentResult> GetAllStudentResult()
        {
            Qurey = "SELECT * FROM studentResult";
            Command = new SqlCommand(Qurey, Connection);
            List<SaveStudentResult> studentResults = new List<SaveStudentResult>();

            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                SaveStudentResult studentResult = new SaveStudentResult();
                studentResult.Id = Convert.ToInt32(Reader["id"]);
                studentResult.StudentId = Convert.ToInt32(Reader["studentId"]);
                studentResult.RegNo = Reader["regNo"].ToString();
                studentResult.CourseId = Convert.ToInt32(Reader["courseId"]);
                studentResult.GradeId = Convert.ToInt32(Reader["gradeId"]);

                studentResults.Add(studentResult);
            }
            Reader.Close();
            Connection.Close();

            return studentResults;
        }

        public int UpdateStudentResultData(SaveStudentResult saveStudentResult)
        {
            Qurey = "UPDATE studentResult SET gradeId = @gradeId WHERE studentId = @studentId";
            Command = new SqlCommand(Qurey, Connection);
            Command.Parameters.AddWithValue("@gradeId", saveStudentResult.GradeId);
            Command.Parameters.AddWithValue("@studentId", saveStudentResult.StudentId);

            Connection.Open();
            RowAffected = Command.ExecuteNonQuery();
            Connection.Close();

            return RowAffected;
        }

        public List<StudentResultDetails> GetAllStudentResultDetails()
        {
            Qurey = "SELECT * FROM studentResultDetails";
            Command = new SqlCommand(Qurey, Connection);
            List<StudentResultDetails> studentResultDetails = new List<StudentResultDetails>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                StudentResultDetails studentResult = new StudentResultDetails();
                studentResult.StudentId = Convert.ToInt32(Reader["studentId"]);
                studentResult.RegistrationNo = Reader["registrationNo"].ToString();
                studentResult.Name = Reader["name"].ToString();
                studentResult.Email = Reader["email"].ToString();
                studentResult.DepartmentName = Reader["departmentName"].ToString();
                studentResult.CourseCode = Reader["courseCode"].ToString();
                studentResult.CourseName = Reader["courseName"].ToString();
                studentResult.GradeLetter = Reader["gradeLetter"].ToString();

                studentResultDetails.Add(studentResult);
            }
            Reader.Close();
            Connection.Close();
            return studentResultDetails;
        } 
    }
}