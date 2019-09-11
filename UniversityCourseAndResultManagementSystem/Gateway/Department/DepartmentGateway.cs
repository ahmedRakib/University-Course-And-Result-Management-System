using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Gateway.Department
{
    public class DepartmentGateway:Gateway
    {
        public int SaveDepartment(Models.Department department)
        {
            Qurey = "INSERT INTO department VALUES(@code, @deptName)";
            Command = new SqlCommand(Qurey, Connection);
            Command.Parameters.AddWithValue("@code", department.Code);
            Command.Parameters.AddWithValue("@deptName", department.DepartmentName);

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public List<Models.Department> GetAllDepartments()
        {
            Qurey = "SELECT * FROM department";
            Command = new SqlCommand(Qurey, Connection);
            List<Models.Department> departments = new List<Models.Department>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Models.Department department = new Models.Department();
                department.Id = Convert.ToInt32(Reader["id"]);
                department.Code = Reader["code"].ToString();
                department.DepartmentName = Reader["departmentName"].ToString();

                departments.Add(department);
            }
            Reader.Close();
            Connection.Close();

            return departments;
        }

        public string GetDeptCodeById(int id)
        {
            Qurey = "SELECT code FROM department WHERE id=@id";
            Command = new SqlCommand(Qurey, Connection);
            string code = "";

            Command.Parameters.AddWithValue("@id", id);
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.HasRows)
            {
                Reader.Read();
                code = Reader["code"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return code;
        }
    }
}