using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway.Classroom
{
    public class ClassroomGateway:Gateway
    {
        public List<Models.Classroom> GetAllClassrooms()
        {
            Qurey = "SELECT * FROM classroom";
            Command = new SqlCommand(Qurey, Connection);
            List<Models.Classroom> classrooms = new List<Models.Classroom>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Models.Classroom classroom = new Models.Classroom();
                classroom.Id = Convert.ToInt32(Reader["id"]);
                classroom.RoomNo = Reader["roomNo"].ToString();

                classrooms.Add(classroom);
            }
            Reader.Close();
            Connection.Close();

            return classrooms;
        } 
        public List<Day> GetAllDays()
        {
            Qurey = "SELECT * FROM day";
            Command = new SqlCommand(Qurey, Connection);
            List<Day> days = new List<Day>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Day day = new Day();
                day.Id = Convert.ToInt32(Reader["id"]);
                day.DayName = Reader["day"].ToString();

                days.Add(day);
            }
            Reader.Close();
            Connection.Close();

            return days;
        }

        public int SaveClassroomAllocation(AllocateClassroom allocateClassroom)
        {
            Qurey = "INSERT INTO allocateClassroom VALUES(@deptId, @courseId, @roomId, @dayId, @from, @to,  @status)";
            Command = new SqlCommand(Qurey, Connection);
            Command.Parameters.AddWithValue("@deptId", allocateClassroom.DepartmentId);
            Command.Parameters.AddWithValue("@courseId", allocateClassroom.CourseId);
            Command.Parameters.AddWithValue("@roomId", allocateClassroom.RoomId);
            Command.Parameters.AddWithValue("@dayId", allocateClassroom.DayId);
            Command.Parameters.AddWithValue("@from", allocateClassroom.StartingTime);
            Command.Parameters.AddWithValue("@to", allocateClassroom.EndingTime);
            Command.Parameters.AddWithValue("@status", "active");

            Connection.Open();
            RowAffected = Command.ExecuteNonQuery();
            Connection.Close();

            return RowAffected;
        }

        public List<AllocateClassroom> GetAllAllocationData()
        {
            Qurey = "SELECT * FROM allocateClassroom where status=@status";
            Command = new SqlCommand(Qurey, Connection);
            Command.Parameters.AddWithValue("@status", "active");
            List<AllocateClassroom> allocateClassrooms = new List<AllocateClassroom>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                AllocateClassroom allocate = new AllocateClassroom();
                allocate.Id = Convert.ToInt32(Reader["id"]);
                allocate.CourseId = Convert.ToInt32(Reader["courseId"]);
                allocate.DepartmentId = Convert.ToInt32(Reader["departmentId"]);
                allocate.DayId = Convert.ToInt32(Reader["dayId"]);
                allocate.RoomId = Convert.ToInt32(Reader["roomId"]);
                allocate.StartingTime = Reader["fromTime"].ToString();
                allocate.EndingTime = Reader["toTime"].ToString();
                //allocate.FromTime = Convert.ToDouble(Reader["from"]);
                //allocate.ToTime = Convert.ToDouble(Reader["to"]);

                allocateClassrooms.Add(allocate);

            }
            Reader.Close();
            Connection.Close();
            return allocateClassrooms;
        }

        public List<ClassScheduleInfo> GetAllClassScheduleInfo()
        {
            Qurey = "SELECT * FROM classScheduleInfo WHERE status=@status";
            Command = new SqlCommand(Qurey, Connection);
            Command.Parameters.AddWithValue("@status", "active");
            List<ClassScheduleInfo> classScheduleInfos = new List<ClassScheduleInfo>();
            Connection.Open();
            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                ClassScheduleInfo classSchedule = new ClassScheduleInfo();
                if (Reader["departmentId"]==DBNull.Value)
                {
                    classSchedule.DepartmentId = 0;
                }
                else
                {
                    classSchedule.DepartmentId = Convert.ToInt32(Reader["departmentId"]);
                }
                if (Reader["courseId"] == DBNull.Value)
                {
                    classSchedule.CourseId = 0;
                }
                else
                {
                    classSchedule.CourseId = Convert.ToInt32(Reader["courseId"]);
                }
                if (Reader["departmentName"] == DBNull.Value)
                {
                    classSchedule.DepartmentName = "N/A";
                }
                else
                {
                    classSchedule.DepartmentName = Reader["departmentName"].ToString();
                }
                if (Reader["courseCode"] == DBNull.Value)
                {
                    classSchedule.CourseCode = "N/A";
                }
                else
                {
                    classSchedule.CourseCode = Reader["courseCode"].ToString();
                }
                if (Reader["courseName"] == DBNull.Value)
                {
                    classSchedule.CourseName = "N/A";
                }
                else
                {
                    classSchedule.CourseName = Reader["courseName"].ToString();
                }
                if (Reader["roomNo"]==DBNull.Value)
                {
                    classSchedule.RoomNo = "N/A";
                }
                else
                {
                    classSchedule.RoomNo = Reader["roomNo"].ToString();
                }
                if (Reader["dayCode"]==DBNull.Value)
                {
                    classSchedule.DayCode = "N/A";
                }
                else
                {
                    classSchedule.DayCode = Reader["dayCode"].ToString();
                }
                if (Reader["dayName"]==DBNull.Value)
                {
                    classSchedule.DayName = "N/A";
                }
                else
                {
                    classSchedule.DayName = Reader["dayName"].ToString();
                }
                if (Reader["fromTime"]==DBNull.Value)
                {
                    classSchedule.FromTime = "N/A";
                }
                else
                {
                    classSchedule.FromTime = Reader["fromTime"].ToString();
                }
                if (Reader["toTime"] == DBNull.Value)
                {
                    classSchedule.ToTime = "N/A";
                }
                else
                {
                    classSchedule.ToTime = Reader["toTime"].ToString();
                }
                
                classScheduleInfos.Add(classSchedule);
            }
            Reader.Close();
            Connection.Close();

            return classScheduleInfos;
        }

        public int UnallocateAllClassrooms()
        {
            Qurey = "UPDATE allocateClassroom SET status=@status WHERE status=@currentStatus";
            Command = new SqlCommand(Qurey, Connection);
            Command.Parameters.AddWithValue("@status", "inactive");
            Command.Parameters.AddWithValue("@currentStatus", "active");
            
            Connection.Open();
            RowAffected = Command.ExecuteNonQuery();
            Connection.Close();

            return RowAffected;
        }
    }
}