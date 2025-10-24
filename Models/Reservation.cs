namespace RailwayReservation.Web.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string PassengerName { get; set; } = "";
        public string FromStation { get; set; } = "";
        public string ToStation { get; set; } = "";
        public DateTime TravelDate { get; set; }
        public string Coach { get; set; } = "";
        public string SeatNumber { get; set; } = "";
    }
}
