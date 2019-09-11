using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Gateway.Student;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Manager.Student
{
    public class StudentResultManager
    {
        StudentResultGateway studentResultGateway = new StudentResultGateway();
        public List<ResultGrade> GetAllResultGrades()
        {
            return studentResultGateway.GetAllResultGrades();
        }

        public string SaveStudentResultData(SaveStudentResult saveStudentResult)
        {
            List<SaveStudentResult> studentResults = studentResultGateway.GetAllStudentResult();
            int count = 0;
            foreach (var result in studentResults)
            {
                if (result.StudentId==saveStudentResult.StudentId)
                {
                    if (result.CourseId==saveStudentResult.CourseId)
                    {
                        count = 1;
                        break;
                    }
                }
            }
            if (count>0)
            {
                if (studentResultGateway.UpdateStudentResultData(saveStudentResult) > 0)
                {
                    return "Student Result Updated Successfully.";
                }
                return "Student Result Update Failed."; 
            }
            else
            {
                if (studentResultGateway.SaveStudentResultData(saveStudentResult) > 0)
                {
                    return "Student Result Saved Successfully.";
                }
                return "Student Result Saving Failed."; 
            }       
        }

        public List<SaveStudentResult> GetAllStudentResult()
        {
            return studentResultGateway.GetAllStudentResult();
        }

        public List<StudentResultDetails> GetAllStudentResultDetails()
        {
            return studentResultGateway.GetAllStudentResultDetails();
        } 
    }
}