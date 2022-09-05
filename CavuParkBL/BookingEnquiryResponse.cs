namespace CavuParkBL
{
    public class BookingEnquiryResponse
    {
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
        public decimal price { get; set; }
        public bool isAvailable { get; set; }
    }
}
