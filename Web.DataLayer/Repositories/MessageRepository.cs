using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Dapper;
using Web.DataLayer.Interface;
using Web.DataLayer.Util;
using Web.Models.ModelView;
using Web.Models.Tables;

namespace Web.DataLayer.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DapperContext _dbContext;
        private readonly HttpClient _client;

        public MessageRepository()
        {
            _client = new HttpClient { BaseAddress = new Uri(UrlString.BaseAddress()) };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            _dbContext = new DapperContext();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<IEnumerable<GuardianContact>> GuardianContact(int? sectionId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@SectionId", sectionId);

            return await _dbContext.Connection.QueryAsync<GuardianContact>("GuardianGetContactNumber", p,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<string> SendMessage(string phoneNumber, string message)
        {
            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("1", phoneNumber),
                    new KeyValuePair<string, string>("2", message),
                    new KeyValuePair<string, string>("3", "JENVE230212_LW782")
                });

                HttpResponseMessage response = await _client.PostAsync("/php_api/api.php", content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<int> InsertSmsDetails(SmsDetails model)
        {
            try
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@SmsType", model.SmsType);
                p.Add("@StudentGuardianId", model.StudentGuardianId);
                p.Add("@AspNetUserId", model.AspNetUserId);
                p.Add("@DateSent", model.DateSent);
                p.Add("@Status", model.Status);
                p.Add("@Message", model.Message);
                p.Add("@SmsDetailsId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _dbContext.Connection.ExecuteAsync("SmsDetailsInsert", p,
                    commandType: CommandType.StoredProcedure);

                return p.Get<int>("@SmsDetailsId");
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public async Task<int> StudentCheckTime(int? id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@StudentId", id);

            return await _dbContext.Connection.ExecuteScalarAsync<int>("StudentTimeCheck", p,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> AttendanceDetailsInsert(int? attendanceHeaderId, int? timeTypeId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@AttendanceDetailsId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@AttendanceHeaderId", attendanceHeaderId);
            p.Add("@TimeTypeId", timeTypeId);
            p.Add("@Date", DateTime.Now);
            p.Add("@Time", DateTime.Now);

            await _dbContext.Connection.ExecuteAsync("AttendanceDetailsInsert", p,
                commandType: CommandType.StoredProcedure);

            return p.Get<int>("@AttendanceDetailsId");
        }

        public async Task<int> AttendanceHeaderInsert(int? studentId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@AttendanceHeaderId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@StudentId", studentId);

            await _dbContext.Connection.ExecuteAsync("AttendanceHeaderInsert", p,
                commandType: CommandType.StoredProcedure);

            return p.Get<int>("@AttendanceHeaderId");
        }

        public async Task AttendanceLogInsert(int? attendanceHeaderId, int? studentGuardianId, string apiResponse)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@AttendanceHeaderId", attendanceHeaderId);
            p.Add("@StudentGuardianId", studentGuardianId);
            p.Add("@ApiResponse", apiResponse);

            await _dbContext.Connection.ExecuteAsync("AttendanceLogInsert", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<AttendanceDetails> SelectAttandanceDetailsId(int? attendanceDetailsId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@AttendanceDetailsId", attendanceDetailsId);

            var result = await _dbContext.Connection.QueryAsync<AttendanceDetails>("AttendanceDetailsById",
                p, commandType: CommandType.StoredProcedure);

            return result.FirstOrDefault();
        }
    }
}
