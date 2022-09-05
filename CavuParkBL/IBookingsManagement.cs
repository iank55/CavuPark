using CavuParkDAL;

namespace CavuParkBL
{
    public interface IBookingsManagement
    {
        Task<List<DailyBookingsSummary>> GetDailyBookingsSummaries(DateTime dateFrom, DateTime dateTo);

        Task<Booking> GetBookingById(int id);

        Task<bool> CancelBooking(int id);

        Task<Booking> AmendBooking(int id, DateTime dateFrom, DateTime dateTo);

        Task<Booking> AddBooking(DateTime dateFrom, DateTime dateTo);

        Task<BookingEnquiryResponse> GetBookingEnquiryResponse(DateTime dateFrom, DateTime dateTo);

    }
}
