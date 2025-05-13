using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventEaseBookingSystem.Data;
using EventEaseBookingSystem.Models;
using EventEaseBookingSystem.Services;

namespace EventEaseBookingSystem.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly BlobService _blobService;

        public EventsController(ApplicationDbContext context, BlobService blobService)
        {
            _context = context;
            _blobService = blobService;
        }

        // GET: Events
        public async Task<IActionResult> Index(string searchString)
        {
            var events = _context.Events.Include(e => e.Venue).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                events = events.Where(e => e.EventName.Contains(searchString));
            }

            return View(await events.ToListAsync());
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName");
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event @event, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                // Prevent double booking: same venue + same date
                bool isDoubleBooked = _context.Events.Any(e =>
                    e.VenueId == @event.VenueId && e.EventDate.Date == @event.EventDate.Date);

                if (isDoubleBooked)
                {
                    ModelState.AddModelError(string.Empty, "This venue is already booked on the selected date.");
                    ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", @event.VenueId);
                    return View(@event);
                }

                if (imageFile != null && imageFile.Length > 0)
                {
                    var imageUrl = await _blobService.UploadFileAsync(imageFile);
                    @event.ImageUrl = imageUrl;
                }

                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", @event.VenueId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Events.FindAsync(id);
            if (@event == null) return NotFound();

            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", @event.VenueId);
            return View(@event);
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event @event, IFormFile imageFile)
        {
            if (id != @event.EventId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Check for double booking (excluding the current event)
                    bool isDoubleBooked = _context.Events.Any(e =>
                        e.EventId != id &&
                        e.VenueId == @event.VenueId &&
                        e.EventDate.Date == @event.EventDate.Date);

                    if (isDoubleBooked)
                    {
                        ModelState.AddModelError(string.Empty, "This venue is already booked on the selected date.");
                        ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", @event.VenueId);
                        return View(@event);
                    }

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var imageUrl = await _blobService.UploadFileAsync(imageFile);
                        @event.ImageUrl = imageUrl;
                    }

                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "VenueName", @event.VenueId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Events
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(e => e.EventId == id);

            if (@event == null) return NotFound();

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);

            if (@event != null)
            {
                bool hasBookings = _context.Bookings.Any(b => b.EventId == id);

                if (hasBookings)
                {
                    ModelState.AddModelError(string.Empty, "You cannot delete this event because it has active bookings.");
                    return RedirectToAction(nameof(Index));
                }

                _context.Events.Remove(@event);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
