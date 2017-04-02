using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NUnit.Framework;
using Web.DataLayer.Util;
using Web.Models.Tables;

namespace Web.UnitTest
{
    [TestFixture]
    public class MultiSelectTest
    {
        [Test]
        public async Task Test()
        {
            DapperContext dbContxt = new DapperContext();
            DynamicParameters p = new DynamicParameters();
            p.Add("@StudentId",3);
            p.Add("@DateFrom", Convert.ToDateTime("2016-10-01"));
            p.Add("@DateTo", Convert.ToDateTime("2016-10-31"));

            var result =
                await
                    dbContxt.Connection.QueryMultipleAsync("ReportSelectByStudentId", p, commandType: CommandType.StoredProcedure);

            Student student = result.ReadAsync<Student>().Result.Single();
            student.AttendanceDetails = result.Read<AttendanceDetails, TimeType, AttendanceDetails>(
                (attendanceDetails, timeType) =>
                {
                    attendanceDetails.TimeType = timeType;
                    return attendanceDetails;
                },  "TimeTypeId");
        }
    }
}
