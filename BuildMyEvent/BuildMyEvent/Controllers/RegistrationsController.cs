using System.Threading.Tasks;
using BuildMyEvent.Data;
using BuildMyEvent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildMyEvent.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistrationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Register(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) return NotFound();

            var ev = await _context.Events.FirstOrDefaultAsync(e => e.Slug == slug);
            if (ev == null) return NotFound();

            ViewBag.EventName = ev.Name;
            ViewBag.Slug = slug;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string slug, string name, string email, string phone)
        {
            if (string.IsNullOrWhiteSpace(slug)) return NotFound();

            var ev = await _context.Events.FirstOrDefaultAsync(e => e.Slug == slug);
            if (ev == null) return NotFound();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                ViewBag.EventName = ev.Name;
                ViewBag.Slug = slug;
                ViewBag.Error = "Name and Email are required.";
                return View();
            }

            var reg = new Registration
            {
                EventId = ev.Id,
                Name = name,
                Email = email,
                Phone = phone
            };

            _context.Registrations.Add(reg);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Thanks), new { slug });
        }

        [HttpGet]
        public async Task<IActionResult> Thanks(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) return NotFound();

            var ev = await _context.Events.FirstOrDefaultAsync(e => e.Slug == slug);
            if (ev == null) return NotFound();

            ViewBag.EventName = ev.Name;
            return View();
        }
    }
}
