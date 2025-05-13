using Microsoft.EntityFrameworkCore;
using EventEaseBookingSystem.Models;

namespace EventEaseBookingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Venue> Venue { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Booking> Booking { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define one-to-many relationships
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Venue)
                .WithMany(static v => v.Event)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Event)  // Booking has one Event
                .WithMany(e => e.Booking)  // Event has many Bookings
                .HasForeignKey(b => b.EventId)  // Foreign key on Booking table
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Venue)
                .WithMany(e => e.Booking)
                .HasForeignKey(b => b.VenueId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevent double booking on same venue/event
            modelBuilder.Entity<Booking>()
                .HasIndex(b => new { b.EventId, b.VenueId })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
