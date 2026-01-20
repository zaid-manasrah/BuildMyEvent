using System.Linq;
using System.Threading.Tasks;
using BuildMyEvent.Data;
using BuildMyEvent.Models;
using BuildMyEvent.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildMyEvent.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool IsAdminUser()
        {
            var userId = HttpContext.Session.GetInt32("CurrentUserId");
            if (userId == null) return false;

            var user = _context.Users.FirstOrDefault(u => u.Id == userId.Value);
            return user != null && user.IsAdmin;
        }

        public async Task<IActionResult> Index()
        {
            if (!IsAdminUser())
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var totalUsers = await _context.Users.CountAsync();
            var totalEvents = await _context.Events.CountAsync();
            var totalRegistrations = await _context.Registrations.CountAsync();

            var topTemplates = await _context.Events
                .GroupBy(e => e.TemplateKey)
                .Select(g => new { TemplateKey = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(5)
                .ToListAsync();

            var latestEvents = await _context.Events
                .OrderByDescending(e => e.Id)
                .Take(5)
                .ToListAsync();

            var latestRegistrations = await _context.Registrations
                .Include(r => r.Event)
                .OrderByDescending(r => r.Id)
                .Take(5)
                .ToListAsync();

            var model = new AdminDashboardViewModel
            {
                TotalUsers = totalUsers,
                TotalEvents = totalEvents,
                TotalRegistrations = totalRegistrations,
                TopTemplates = topTemplates
                    .Select(t => new TemplateUsageViewModel
                    {
                        TemplateKey = t.TemplateKey,
                        Count = t.Count
                    })
                    .ToList(),
                LatestEvents = latestEvents,
                LatestRegistrations = latestRegistrations
            };

            return View(model);
        }

        public async Task<IActionResult> Users()
        {
            if (!IsAdminUser())
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var users = await _context.Users
                .Select(u => new AdminUserListItemViewModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    IsAdmin = u.IsAdmin,
                    IsActive = u.IsActive,
                    EventsCount = u.Events.Count
                })
                .OrderBy(u => u.Name)
                .ToListAsync();

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleUserStatus(int id)
        {
            if (!IsAdminUser())
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.IsActive = !user.IsActive;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Users));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!IsAdminUser())
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var user = await _context.Users
                .Include(u => u.Events)
                .ThenInclude(e => e.Registrations)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Users));
        }

        public async Task<IActionResult> Events()
        {
            if (!IsAdminUser())
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var events = await _context.Events
                .Include(e => e.OwnerUser)
                .Include(e => e.Registrations)
                .OrderByDescending(e => e.Id)
                .ToListAsync();

            return View(events);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            if (!IsAdminUser())
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var ev = await _context.Events
                .Include(e => e.Registrations)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (ev == null)
            {
                return NotFound();
            }

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Events));
        }

        public async Task<IActionResult> Registrations(int? eventId)
        {
            if (!IsAdminUser())
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var query = _context.Registrations
                .Include(r => r.Event)
                .AsQueryable();

            if (eventId.HasValue)
            {
                query = query.Where(r => r.EventId == eventId.Value);
            }

            var events = await _context.Events
                .OrderBy(e => e.Name)
                .ToListAsync();

            var registrations = await query
                .OrderByDescending(r => r.Id)
                .ToListAsync();

            var model = new AdminRegistrationsViewModel
            {
                Events = events,
                SelectedEventId = eventId,
                Registrations = registrations
            };

            return View(model);
        }
    }
}
