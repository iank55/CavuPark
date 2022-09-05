using CavuParkDAL;
using Microsoft.Extensions.Configuration;

namespace CavuParkBL
{
    public class BookingsManagement : IBookingsManagement
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPricesRepository _pricesRepository;
        private readonly int _spacesAvailable;
        private readonly decimal _fallbackPrice;

        public BookingsManagement(IBookingRepository bookingRepository, IPricesRepository pricesRepository, IConfiguration configuration)
        {
            _bookingRepository = bookingRepository;
            _pricesRepository = pricesRepository;
            _spacesAvailable = int.Parse(configuration.GetSection("SpacesAvailable").Value);
            _fallbackPrice = decimal.Parse(configuration.GetSection("FallbackPrice").Value);
        }

        public async Task<Booking> AddBooking(DateTime dateFrom, DateTime dateTo)
        {
            return await _bookingRepository.AddBooking(dateFrom, dateTo);
        }

        public async Task<Booking> AmendBooking(int id, DateTime dateFrom, DateTime dateTo)
        {
            return await _bookingRepository.AmendBooking(id, dateFrom, dateTo);
        }

        public async Task<bool> CancelBooking(int id)
        {
            return await _bookingRepository.CancelBooking(id);
        }

        public async Task<Booking> GetBookingById(int id)
        {
            return await _bookingRepository.GetBooking(id);
        }

        public async Task<BookingEnquiryResponse> GetBookingEnquiryResponse(DateTime dateFrom, DateTime dateTo)
        {
            var bookingEnquiryResponse = new BookingEnquiryResponse() 
                { 
                    dateFrom = dateFrom, 
                    dateTo = dateTo,
                    isAvailable = true
                };
            
            var bookings = await _bookingRepository.GetBookings(dateFrom, dateTo);
            var prices = await _pricesRepository.GetPrices(dateFrom, dateTo);

            for (var day = dateFrom.Date; day.Date <= dateTo.Date; day = day.AddDays(1))
            {
                var bookingsToday = bookings.Where(x => !x.Cancelled &&
                   (day >= x.DateFrom && day <= x.DateTo)).Count();

                var todaysPrice = prices.Single(x => day >= x.DateFrom && day <= x.DateTo).Price;

                bookingEnquiryResponse.price += todaysPrice != 0 ? todaysPrice : _fallbackPrice;

                if (bookingsToday == _spacesAvailable) bookingEnquiryResponse.isAvailable = false;

            }
            return bookingEnquiryResponse;
        }

        public async Task<List<DailyBookingsSummary>> GetDailyBookingsSummaries(DateTime dateFrom, DateTime dateTo)
        {
            var dailyBookingSummaries = new List<DailyBookingsSummary>();
            var bookings = await _bookingRepository.GetBookings(dateFrom, dateTo);

            for (var day = dateFrom.Date; day.Date <= dateTo.Date; day = day.AddDays(1))
            {
                var bookingsToday = bookings.Where(x => !x.Cancelled &&
                   (day >= x.DateFrom && day <= x.DateTo)).Count();

                dailyBookingSummaries.Add(new DailyBookingsSummary() { Date = day, Summary = BookingSummaryText(bookingsToday) });
            }


            return dailyBookingSummaries;
        }

        private string BookingSummaryText(int numberOfBookings)
        {
            if (numberOfBookings == _spacesAvailable) { return "no free spaces"; }
            if (numberOfBookings == 0) { return "all free spaces"; }
            return $"{_spacesAvailable - numberOfBookings} free spaces";
        }
    }
}

