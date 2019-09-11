using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using UniversityCourseAndResultManagementSystem.Gateway.Classroom;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Manager.Classroom
{
    public class ClassroomManager
    {
        private ClassroomGateway classroomGateway = new ClassroomGateway();

        public List<Models.Classroom> GetAllClassrooms()
        {
            return classroomGateway.GetAllClassrooms();
        }

        public List<Day> GetAllDays()
        {
            return classroomGateway.GetAllDays();
        }

        public string SaveClassroomAllocation(AllocateClassroom allocateClassroom)
        {
            List<AllocateClassroom> allocationData = GetAllAllocationData();
            int count = 0;
            string fromTime = allocateClassroom.StartingTime.Substring(0, 5);
            string fromMeridian = allocateClassroom.StartingTime.Substring(6, 2);
            string[] result1 = fromTime.Split(':');
            int fromHours = Convert.ToInt32(result1[0]);
            int fromMinutes = Convert.ToInt32(result1[1]);

            string toTime = allocateClassroom.EndingTime.Substring(0, 5);
            string toMeridian = allocateClassroom.EndingTime.Substring(6, 2);
            string[] result2 = toTime.Split(':');
            int toHours = Convert.ToInt32(result2[0]);
            int toMinutes = Convert.ToInt32(result2[1]);

            foreach (var allocation in allocationData)
            {
                if (allocation.DayId == allocateClassroom.DayId)
                {
                    string fromTimeStore = allocation.StartingTime.Substring(0, 5);
                    string fromMeridianStore = allocation.StartingTime.Substring(6, 2);
                    string[] result1Store = fromTimeStore.Split(':');
                    int fromHoursStore = Convert.ToInt32(result1Store[0]);
                    int fromMinutesStore = Convert.ToInt32(result1Store[1]);

                    string toTimeStore = allocation.EndingTime.Substring(0, 5);
                    string toMeridianStore = allocation.EndingTime.Substring(6, 2);
                    string[] result2Store = toTimeStore.Split(':');
                    int toHoursStore = Convert.ToInt32(result2Store[0]);
                    int toMinutesStore = Convert.ToInt32(result2Store[1]);
                    if (fromMeridian == fromMeridianStore || fromMeridian == toMeridianStore)
                    {
                        if (fromHours > fromHoursStore && fromHours < toHoursStore)
                        {
                            count = 1;
                            break;
                        }
                        else if (fromHours == fromHoursStore)
                        {
                            if (fromMinutes >= fromMinutesStore)
                            {
                                count = 1;
                                break;
                            }
                        }
                        else if (fromHours == toHoursStore)
                        {
                            if (fromMinutes < toMinutesStore)
                            {
                                count = 1;
                                break;
                            }
                        }
                    }
                    if (toMeridian == fromMeridianStore || toMeridian == toMeridianStore)
                    {
                        if (toHours > fromHoursStore && toHours < toHoursStore)
                        {
                            count = 1;
                            break;
                        }
                        else if (toHours == fromHoursStore)
                        {
                            if (toMinutes > fromMinutesStore)
                            {
                                count = 1;
                                break;
                            }
                        }
                        else if (toHours == toHoursStore)
                        {
                            if (toMinutes <= toMinutesStore)
                            {
                                count = 1;
                                break;
                            }
                        }
                    }
                }

            }

            if (count == 1)
            {
                return "Schedule not availabale";
            }
            else
            {
                if (classroomGateway.SaveClassroomAllocation(allocateClassroom) > 0)
                {
                    return "Classroom allocated successfully";
                }
                return "classroom allocation failed";
            }
        }

        public List<AllocateClassroom> GetAllAllocationData()
        {
            return classroomGateway.GetAllAllocationData();
        }

        public List<ClassScheduleInfo> GetAllClassScheduleInfo()
        {
            List<ClassScheduleInfo> classScheduleInfos = classroomGateway.GetAllClassScheduleInfo();
            List<ClassScheduleInfo> classScheduleInfos2 = new List<ClassScheduleInfo>();
            foreach (var schedule in classScheduleInfos)
            {
                ClassScheduleInfo classSchedule = new ClassScheduleInfo();
                classSchedule.CourseId = schedule.CourseId;
                classSchedule.CourseCode = schedule.CourseCode;
                classSchedule.CourseName = schedule.CourseName;
                classSchedule.DepartmentId = schedule.DepartmentId;
                classSchedule.DepartmentName = schedule.DepartmentName;
                classSchedule.RoomNo = schedule.RoomNo;
                classSchedule.DayCode = schedule.DayCode;
                classSchedule.DayName = schedule.DayName;
                classSchedule.FromTime = schedule.FromTime;
                classSchedule.ToTime = schedule.ToTime;
                if (classSchedule.RoomNo == "N/A" && classSchedule.DayCode == "N/A" && classSchedule.DayName == "N/A" && classSchedule.FromTime == "N/A" && classSchedule.ToTime == "N/A")
                {
                    classSchedule.ScheduleDetails = "Not Scheduled Yet";
                }
                else
                {
                    classSchedule.ScheduleDetails = "R.No: " + schedule.RoomNo + ", " + schedule.DayCode + ", " +
                                                schedule.FromTime + " - " + schedule.ToTime + ";<br/>";
                }

                classScheduleInfos2.Add(classSchedule);
            }
            return classScheduleInfos2;
        }

        public string UnallocateAllClassrooms()
        {
            if (classroomGateway.UnallocateAllClassrooms()>0)
            {
                return "Classrooms unallocated successfully";
            }
            return "Failed to unallocate the classrooms";
        }
    }
}
