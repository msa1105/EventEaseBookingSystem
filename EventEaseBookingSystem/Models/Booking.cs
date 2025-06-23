namespace EventEaseBookingSystem.Models;

using System.ComponentModel.DataAnnotations;
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

public class EventType
{
    [Key]
    public int EventTypeId { get; set; }

    [Required]
    public string Name { get; set; }

    // Optional: Navigation property
    public ICollection<Event> Events { get; set; }
}
