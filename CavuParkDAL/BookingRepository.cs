using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CavuParkDAL
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IConfiguration _configuration;
        public BookingRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Booking> AddBooking(DateTime dateFrom, DateTime dateTo)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("DateFrom", dateFrom, System.Data.DbType.DateTime);
            parameters.Add("DateTo", dateTo, System.Data.DbType.DateTime);

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var booking = await connection.QueryFirstOrDefaultAsync<Booking>("AddBooking", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return booking;
            }
        }

        public async Task<Booking> AmendBooking(int id, DateTime dateFrom, DateTime dateTo)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id);
            parameters.Add("DateFrom", dateFrom, System.Data.DbType.DateTime);
            parameters.Add("DateTo", dateTo, System.Data.DbType.DateTime);

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var booking = await connection.QueryFirstOrDefaultAsync<Booking>("UpdateBooking", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return booking;
            }
        }

        public async Task<bool> CancelBooking(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id);

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                await connection.ExecuteAsync("CancelBooking", parameters, commandType: System.Data.CommandType.StoredProcedure);       
                return true;    
            }
        }

        public async Task<Booking> GetBooking(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id);

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var booking = await connection.QueryFirstOrDefaultAsync<Booking>("GetBookingById", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return booking;
            }
        }

        public async Task<IEnumerable<Booking>> GetBookings(DateTime dateFrom, DateTime dateTo)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("DateFrom", dateFrom, System.Data.DbType.DateTime);
            parameters.Add("DateTo", dateTo, System.Data.DbType.DateTime);

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var bookings = await connection.QueryAsync<Booking>("GetBookingsInDateRange", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return bookings;
            }
        }
    }
}
