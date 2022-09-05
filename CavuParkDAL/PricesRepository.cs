using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace CavuParkDAL
{
    public class PricesRepository : IPricesRepository
    {
        private readonly IConfiguration _configuration;
        public PricesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Prices>> GetPrices(DateTime dateFrom, DateTime dateTo)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("DateFrom", dateFrom, System.Data.DbType.DateTime);
            parameters.Add("DateTo", dateTo, System.Data.DbType.DateTime);

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var bookings = await connection.QueryAsync<Prices>("GetPricesInDateRange", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return bookings;
            }
        }
    }
}
