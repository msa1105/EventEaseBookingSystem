using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventEaseBookingSystem.Models
{
    public class Venue
    {
        public int VenueId { get; set; }

        [Required]
        public string VenueName { get; set; }

        public string Location { get; set; }

        public int Capacity { get; set; }

        public string ImageUrl { get; set; }

        // Navigation property
        public ICollection<Event> Events { get; set; }
    }
}
