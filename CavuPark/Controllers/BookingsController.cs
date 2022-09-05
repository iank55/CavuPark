using CavuParkBL;
using CavuParkDAL;
using Microsoft.AspNetCore.Mvc;

namespace CavuPark.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly ILogger<ManagementController> _logger;
        private readonly IBookingsManagement _bookingsManagement;

        public BookingsController(ILogger<ManagementController> logger, IBookingsManagement bookingsManagement)
        {
            _logger = logger;
            _bookingsManagement = bookingsManagement;
        }

        [HttpGet(Name = "~/GetBookingEnquiry")]
        public async Task<BookingEnquiryResponse> GetBookingEnquiryAsync(DateTime fromDate, DateTime toDate)
        {
            return await _bookingsManagement.GetBookingEnquiryResponse(fromDate, toDate);
        }

        //[HttpGet(Name = "~/GetBooking")]
        //public async Task<Booking> GetAsync(int id)
        //{
        //    return await _bookingsManagement.GetBookingById(id);
        //}

        [HttpPost(Name = "AddBooking")]
        public async Task<Booking> AddAsync(DateTime fromDate, DateTime toDate)
        {
            return await _bookingsManagement.AddBooking(fromDate, toDate);
        }

        [HttpDelete(Name = "CancelBooking")]
        public async Task<bool> CancelAsync(int id)
        {
            return await _bookingsManagement.CancelBooking(id);
        }

        [HttpPatch(Name = "AmendBooking")]
        public async Task<Booking> AmendAsync(int id, DateTime fromDate, DateTime toDate)
        {
            return await _bookingsManagement.AmendBooking(id, fromDate, toDate);
        }
    }
}
