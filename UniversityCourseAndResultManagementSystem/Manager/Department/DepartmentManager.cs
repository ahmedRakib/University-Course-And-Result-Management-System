using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Gateway.Department;

namespace UniversityCourseAndResultManagementSystem.Manager.Department
{
    public class DepartmentManager
    {
        DepartmentGateway departmentGateway = new DepartmentGateway();

        public string SaveDepartment(Models.Department department)
        {
            if (departmentGateway.SaveDepartment(department)>0)
            {
                return "Department Saved Successfully.";
            }
            return "Department saving Failed.";
        }

        public List<Models.Department> GetAllDepartments()
        {
            return departmentGateway.GetAllDepartments();
        }

        public string GetDeptCodeById(int id)
        {
            return departmentGateway.GetDeptCodeById(id);
        }
    }
}