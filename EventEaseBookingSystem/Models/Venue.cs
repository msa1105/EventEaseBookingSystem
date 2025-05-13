using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEaseBookingSystem.Models
{
    public class Venue
    {
        public int VenueId { get; set; }
        [Required]
        public required string VenueName { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
     
        [StringLength(255)]
        public string ImageUrl { get; set; }

        [NotMapped]
        [Display(Name = "Venue Image")]
        public IFormFile ImageFile { get; set; }
        // Navigation property
        public ICollection<Event> Event { get; set; }
        public ICollection<Booking> Booking { get; set; } 
    }
}
