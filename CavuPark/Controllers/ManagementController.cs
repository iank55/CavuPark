using CavuParkBL;
using Microsoft.AspNetCore.Mvc;

namespace CavuPark.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManagementController : ControllerBase
    {
        private readonly ILogger<ManagementController> _logger;
        private readonly IBookingsManagement _bookingsManagement;

        public ManagementController(ILogger<ManagementController> logger, IBookingsManagement bookingsManagement)
        {
            _logger = logger;
            _bookingsManagement = bookingsManagement;
        }

        [HttpGet(Name = "GetBookings")]
        public async Task<IEnumerable<DailyBookingsSummary>> GetAsync(DateTime fromDate, DateTime toDate)
        {
            return await _bookingsManagement.GetDailyBookingsSummaries(fromDate, toDate);
        }
    }
}
