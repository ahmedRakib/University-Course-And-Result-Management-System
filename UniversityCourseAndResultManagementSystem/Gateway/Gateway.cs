using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UniversityCourseAndResultManagementSystem.Gateway
{
    public class Gateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDb"].ConnectionString;
        public SqlConnection Connection { get; set; }
        public string Qurey { get; set; }
        public SqlCommand Command { get; set; }
        public SqlDataReader Reader { get; set; }

        public int RowAffected { get; set; }
        public Gateway()
        {
            Connection=new SqlConnection(connectionString);
        }
    }
}