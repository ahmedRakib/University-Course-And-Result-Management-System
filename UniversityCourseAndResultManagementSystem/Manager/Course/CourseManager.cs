using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Gateway.Course;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Manager.Course
{
    public class CourseManager
    {
        CourseGateway courseGateway = new CourseGateway();
        public List<Semester> GetAllSemesters()
        {
            return courseGateway.GetAllSemesters();
        }

        public string SaveCourse(Models.Course course)
        {
            if (courseGateway.SaveCourse(course)>0)
            {
                return "Course Saved Successfully.";
            }
            return "Course Saving Failed";
        }

        public List<Models.Course> GetAllCourses()
        {
            return courseGateway.GetAllCourses();
        }

        public string AssignCourse(int teacherId, int courseId)
        {
            List<CourseDetailsWithTDS> courseDetailsWithTds = courseGateway.GetAllAssignedCourses();
            int count = 0;
            foreach (var course in courseDetailsWithTds)
            {
                if (course.CourseId==courseId)
                {
                    count = 1;
                }
            }
            if (count==1)
            {
                return "Course already assigned";
            }
            else
            {
                if (courseGateway.AssignCourse(teacherId, courseId) > 0)
                {
                    return "Course Assigned Successfully";
                }
                return "Course Assigning Failed";
            }
        }

        public List<CourseDetailsWithTDS> GetCourseDetails()
        {
            return courseGateway.GetCourseDetails();
        }

        public List<CourseWithAllocationId> GetAllCourseWithAllocation()
        {
            return courseGateway.GetAllCourseWithAllocationId();
        }

        public string UnassignAllCourses()
        {
            if (courseGateway.UnassignAllCourses()>0)
            {
                return "All Courses Unassigned";
            }
            return "Failed to unassigned all courses";
        }

        public List<CourseDetailsWithTDS> GetAllCourseWithSemester()
        {
            return courseGateway.GetAllCourseWithSemester();
        } 
    }
}