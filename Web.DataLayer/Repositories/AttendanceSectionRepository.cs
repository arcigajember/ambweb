using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Web.DataLayer.Interface;
using Web.DataLayer.Util;
using Web.Models.ModelView;
using Web.Models.Tables;

namespace Web.DataLayer.Repositories
{
    public class AttendanceSectionRepository : IAttendanceSectionRepository
    {
        private readonly DapperContext _dbContext;

        public AttendanceSectionRepository()
        {
            _dbContext = new DapperContext();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<IEnumerable<TimeType>> AttendanceTimeType(int? timeTypeId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@TimeTypeId", timeTypeId);

            return await _dbContext.Connection.QueryAsync<TimeType>("TimeTypeSelectId", p,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Student>> AttendanceToday()
        {
            return await _dbContext.Connection.QueryAsync<Student, Section, Student>("AttendanceToday",
                (student, section) =>
                {
                    student.Section = section;
                    return student;
                }, commandType: CommandType.StoredProcedure,
                splitOn: "SectionId");
        }

        public async Task<IEnumerable<AttendanceDetails>> AttendanceDetails(int? studentId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@StudentId", studentId);

            return await _dbContext.Connection.QueryAsync<AttendanceDetails>("AttandanceDetailsByStudentId", p,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Student>> AttendanceSection(int? sectionId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@SectionId", sectionId);

            return await _dbContext.Connection.QueryAsync<Student, Section, Room, Student>("AttendanceReportBySection",
                (student, section, room) =>
                {
                    student.Section = section;
                    student.Room = room;
                    return student;
                }, p, commandType: CommandType.StoredProcedure,
                splitOn: "SectionId, RoomId");
        }

        public async Task<IEnumerable<AttendanceDetails>> AttendanceStudent(int? studentId, DateTime? dateFrom, DateTime? dateTo)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@StudentId", studentId);
            p.Add("@DateFrom", dateFrom);
            p.Add("@DateTo", dateTo);

            return
                await
                    _dbContext.Connection.QueryAsync<AttendanceDetails, TimeType, AttendanceDetails>(
                        "AttendanceReportByStudent",
                        (attendanceDetails, timeType) =>
                        {
                            attendanceDetails.TimeType = timeType;
                            return attendanceDetails;
                        }, p, commandType: CommandType.StoredProcedure,
                        splitOn: "TimeTypeId");

        }

        public async Task<Student> AttendanceReportStudent(int? studentId, DateTime? dateFrom, DateTime? dateTo)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@StudentId", studentId);
            p.Add("@DateFrom", dateFrom);
            p.Add("@DateTo", dateTo);

            using (
                var multi =
                    await
                        _dbContext.Connection.QueryMultipleAsync("ReportSelectByStudentId", p,
                            commandType: CommandType.StoredProcedure))
            {

                Student student = multi.Read<Student, Section, Room, Student>(
                    (stu, section, room) =>
                    {
                        stu.Section = section;
                        stu.Room = room;
                        return stu;
                    }, "SectionId, RoomId").SingleOrDefault();


                if (student != null)
                    student.AttendanceDetails = multi.Read<AttendanceDetails, TimeType, AttendanceDetails>(
                        (attendanceDetails, timeType) =>
                        {
                            attendanceDetails.TimeType = timeType;
                            return attendanceDetails;
                        }, "TimeTypeId");


                return student;
            }
        }

        public async Task<IEnumerable<AttendanceLog>> AttendanceLog(MessageLogView modelView)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@DateFrom", modelView.DateFrom);
            p.Add("@DateTo", modelView.DateTo);

            return await
                _dbContext.Connection
                    .QueryAsync
                    <AttendanceLog, Student, Section, Room, TimeType, AttendanceDetails, Guardian, AttendanceLog>(
                        "AttendanceLogDetails", (
                            log, student, section, room, timetype, attendancedetails, guardian) =>
                        {
                            log.Student = student;
                            log.Student.Section = section;
                            log.Student.Room = room;
                            log.AttendanceDetails = attendancedetails;
                            log.AttendanceDetails.TimeType = timetype;
                            log.Guardian = guardian;
                            return log;
                        }, p, commandType: CommandType.StoredProcedure,
                        splitOn: "StudentId, SectionId, RoomId, TimeTypeId, AttendanceDetailsId, GuardianId");
        }

        public async Task<IEnumerable<SmsDetails>> SmsLog(MessageLogView modelView)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@DateFrom", modelView.DateFrom);
            p.Add("@DateTo", modelView.DateTo);

            return
                await
                    _dbContext.Connection
                        .QueryAsync<SmsDetails, AspNetUsers, Student, Section, Room, Guardian, SmsDetails>(
                            "SmsDetailsByDate",
                            (smsDetails, aspNet, student, section, room, guardian) =>
                            {
                                smsDetails.Student = student;
                                smsDetails.Student.Section = section;
                                smsDetails.Student.Room = room;
                                smsDetails.AspNetUsers = aspNet;
                                smsDetails.Guardian = guardian;
                                return smsDetails;
                            }, p, commandType: CommandType.StoredProcedure,
                            splitOn: "Id, StudentId, SectionId, RoomId, GuardianId");
        }
    }
}
