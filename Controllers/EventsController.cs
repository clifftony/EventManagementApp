using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EventManagementApp.Data;
using EventManagementApp.Models;
using EventManagementApp.Models.ViewModels;

namespace EventManagementApp.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var events = await _context.Events
                .Where(e => e.UserId == userId)
                .ToListAsync();

            // Sort on client side since SQLite doesn't support TimeSpan in ORDER BY
            events = events
                .OrderByDescending(e => e.EventDate)
                .ThenByDescending(e => e.EventTime)
                .ToList();

            return View(events);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var @event = await _context.Events.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var @event = new Event
                {
                    Title = model.Title,
                    Description = model.Description,
                    EventDate = model.EventDate,
                    EventTime = model.EventTime,
                    Location = model.Location,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var @event = await _context.Events.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (@event == null)
            {
                return NotFound();
            }

            var model = new EditEventViewModel
            {
                Id = @event.Id,
                Title = @event.Title,
                Description = @event.Description,
                EventDate = @event.EventDate,
                EventTime = @event.EventTime,
                Location = @event.Location
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditEventViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var @event = await _context.Events.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

                    if (@event == null)
                    {
                        return NotFound();
                    }

                    @event.Title = model.Title;
                    @event.Description = model.Description;
                    @event.EventDate = model.EventDate;
                    @event.EventTime = model.EventTime;
                    @event.Location = model.Location;
                    @event.UpdatedAt = DateTime.UtcNow;

                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var @event = await _context.Events.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var @event = await _context.Events.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
