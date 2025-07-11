﻿using System.ComponentModel.DataAnnotations;

namespace EventEaseBookingSystem.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Required]
        [StringLength(100)]
        public string EventName { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        // Foreign key
        public int VenueId { get; set; }
        public Venue? Venue { get; set; }

        // Foreign key
        public int EventTypeId { get; set; }

        // Navigation property
        public EventType EventType { get; set; }



        public ICollection<Booking>? Booking { get; set; }
    }
}
