namespace EventEaseBookingSystem.Models;
using EventEaseBookingSystem.Models;

public class Booking
{
    public int BookingId { get; set; }

    public int EventId { get; set; }
    public required Event Event { get; set; }

    public int VenueId { get; set; }
    public required Venue Venue { get; set; }

    public DateTime BookingDate { get; set; }


}
