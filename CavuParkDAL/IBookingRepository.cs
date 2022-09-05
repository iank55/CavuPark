using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CavuParkDAL
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetBookings(DateTime dateFrom, DateTime dateTo);

        Task<Booking> GetBooking(int id);

        Task<Booking> AddBooking(DateTime dateFrom, DateTime dateTo);

        Task<Booking> AmendBooking(int id, DateTime dateFrom, DateTime dateTo);

        Task<bool> CancelBooking(int id);


    }
}
