using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway.Department
{
    public class TeacherGateway : Gateway
    {
        public List<Designation> GetAllDesignations()
        {
            Qurey = "SELECT * FROM designation";
            Command = new SqlCommand(Qurey, Connection);
            List<Designation> designations = new List<Designation>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Designation designation = new Designation();
                designation.Id = Convert.ToInt32(Reader["id"]);
                designation.DesignationName = Reader["designation"].ToString();

                designations.Add(designation);
            }
            Reader.Close();
            Connection.Close();
            return designations;
        }

        public int SaveTeacher(Teacher teacher)
        {
            Qurey = "INSERT INTO teacher VALUES(@name, @address, @email, @contact, @designationId, @departmentId, @allotedCredit, @remainingCredit)";
            Command = new SqlCommand(Qurey, Connection);

            Command.Parameters.AddWithValue("@name", teacher.Name);
            Command.Parameters.AddWithValue("@address", teacher.Address);
            Command.Parameters.AddWithValue("@email", teacher.Email);
            Command.Parameters.AddWithValue("@contact", teacher.ContactNo);
            Command.Parameters.AddWithValue("@designationId", teacher.DesignationId);
            Command.Parameters.AddWithValue("@departmentId", teacher.DepartmentId);
            Command.Parameters.AddWithValue("@allotedCredit", teacher.AllottedCredit);
            Command.Parameters.AddWithValue("@remainingCredit", teacher.AllottedCredit);

            Connection.Open();
            RowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return RowAffected;
        }

        public List<Teacher> GetAllTeachers()
        {
            Qurey = "SELECT * FROM teacher";
            Command = new SqlCommand(Qurey, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Teacher> teachers = new List<Teacher>();
            while (Reader.Read())
            {
                Teacher teacher = new Teacher();
                teacher.Id = Convert.ToInt32(Reader["id"]);
                teacher.Name = Reader["name"].ToString();
                teacher.Address = Reader["address"].ToString();
                teacher.Email = Reader["email"].ToString();
                teacher.ContactNo = Reader["contactNo"].ToString();
                teacher.DesignationId = Convert.ToInt32(Reader["designationId"]);
                teacher.DepartmentId = Convert.ToInt32(Reader["departmentId"]);
                teacher.AllottedCredit = Convert.ToDouble(Reader["allottedCredit"]);
                teacher.RemainingCredit = Convert.ToDouble(Reader["remainingCredit"]);

                teachers.Add(teacher);
            }
            Reader.Close();
            Connection.Close();

            return teachers;
        }

        public int UpdateCreditStatus(int teacherId, double credit)
        {
            Qurey = "UPDATE teacher SET remainingCredit=@remainingCredit WHERE id=@teacherId" ;
            Command = new SqlCommand(Qurey, Connection);
            Command.Parameters.AddWithValue("@remainingCredit", credit);
            Command.Parameters.AddWithValue("@teacherId", teacherId);

            Connection.Open();
            RowAffected = Command.ExecuteNonQuery();
            Connection.Close();

            return RowAffected;
        }

        public void ResetCreditStatus()
        {
            List<Teacher> teachers = GetAllTeachers();
            foreach (var teacher in teachers)
            {
                Qurey = "UPDATE teacher SET remainingCredit=@credit WHERE id=@id";
                Command= new SqlCommand(Qurey, Connection);
                Command.Parameters.AddWithValue("@credit", teacher.AllottedCredit);
                Command.Parameters.AddWithValue("@id", teacher.Id);

                Connection.Open();
                RowAffected = Command.ExecuteNonQuery();
                Connection.Close();
            }
        }

        public List<CourseWithAssignedTeacher> CourseWithAssignedTeachers()
        {
            Qurey = "SELECT * FROM courseWithAssignedTeacher WHERE status=@status";
            Command = new SqlCommand(Qurey, Connection);
            Command.Parameters.AddWithValue("@status", "active");
            List<CourseWithAssignedTeacher> courseWithAssignedTeachers = new List<CourseWithAssignedTeacher>();
            
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                CourseWithAssignedTeacher courseWithAssigned = new CourseWithAssignedTeacher();
                courseWithAssigned.AssignId = Convert.ToInt32(Reader["assignId"]);
                courseWithAssigned.CourseId = Convert.ToInt32(Reader["courseId"]);
                courseWithAssigned.TeacherId = Convert.ToInt32(Reader["teacherId"]);
                courseWithAssigned.TeacherName = Reader["name"].ToString();
                courseWithAssigned.Semester = Reader["semester"].ToString();

                courseWithAssignedTeachers.Add(courseWithAssigned);
            }
            Reader.Close();
            Connection.Close();

            return courseWithAssignedTeachers;
        }
    }
}