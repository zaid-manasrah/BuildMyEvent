using System.Threading.Tasks;
using BuildMyEvent.Data;
using BuildMyEvent.Models;
using BuildMyEvent.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace BuildMyEvent.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = HttpContext.Session.GetInt32("CurrentUserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var slug = await GenerateUniqueSlug(model.Name);

            var newEvent = new Event
            {
                Name = model.Name,
                Description = model.Description,
                LogoPath = null,
                Slug = slug,
                TemplateKey = model.TemplateKey,
                OwnerUserId = userId
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { slug = newEvent.Slug });
        }

        [HttpGet]
        public async Task<IActionResult> MyEvents()
        {
            var userId = HttpContext.Session.GetInt32("CurrentUserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var events = await _context.Events
                .Include(e => e.Registrations)
                .Where(e => e.OwnerUserId == userId)
                .OrderByDescending(e => e.Id)
                .ToListAsync();

            return View(events);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("CurrentUserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var ev = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == id);

            if (ev == null)
            {
                return NotFound();
            }

            if (ev.OwnerUserId != userId)
            {
                return Forbid();
            }

            var registrations = _context.Registrations.Where(r => r.EventId == id);
            _context.Registrations.RemoveRange(registrations);

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MyEvents));
        }

        [HttpGet]
        public async Task<IActionResult> Attendees(int id)
        {
            var userId = HttpContext.Session.GetInt32("CurrentUserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var ev = await _context.Events
                .Include(e => e.OwnerUser)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (ev == null)
            {
                return NotFound();
            }

            if (ev.OwnerUserId != userId)
            {
                return Forbid();
            }

            var registrations = await _context.Registrations
                .Where(r => r.EventId == id)
                .OrderByDescending(r => r.Id)
                .ToListAsync();

            ViewBag.EventName = ev.Name;
            return View(registrations);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return NotFound();
            }

            var ev = await _context.Events
                .Include(e => e.Registrations)
                .FirstOrDefaultAsync(e => e.Slug == slug);

            if (ev == null)
            {
                return NotFound();
            }

            var currentUserId = HttpContext.Session.GetInt32("CurrentUserId");
            bool isOwner = currentUserId.HasValue && ev.OwnerUserId == currentUserId.Value;

            ViewBag.IsOwner = isOwner;
            ViewBag.RegistrationsCount = ev.Registrations?.Count ?? 0;

            return View(ev);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("CurrentUserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var ev = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == id);

            if (ev == null)
            {
                return NotFound();
            }

            if (ev.OwnerUserId != userId)
            {
                return Forbid();
            }

            var model = new EditEventViewModel
            {
                Id = ev.Id,
                Name = ev.Name,
                Description = ev.Description,
                LogoFileName = ev.LogoPath,
                Slug = ev.Slug,
                TemplateKey = ev.TemplateKey,
                TechProWhyAttendTitle = ev.TechProWhyAttendTitle,
                TechProWhyAttendPoint1 = ev.TechProWhyAttendPoint1,
                TechProWhyAttendPoint2 = ev.TechProWhyAttendPoint2,
                TechProWhyAttendPoint3 = ev.TechProWhyAttendPoint3,
                TechProCard1Title = ev.TechProCard1Title,
                TechProCard1Body = ev.TechProCard1Body,
                TechProCard2Title = ev.TechProCard2Title,
                TechProCard2Body = ev.TechProCard2Body,
                TechProCard3Title = ev.TechProCard3Title,
                TechProCard3Body = ev.TechProCard3Body,
                CreativeHeroTitle = ev.CreativeHeroTitle,
                CreativeHeroSubtitle = ev.CreativeHeroSubtitle,
                CreativeCtaText = ev.CreativeCtaText,
                CreativeShareTitle = ev.CreativeShareTitle,
                BusinessHeroBadge = ev.BusinessHeroBadge,
                BusinessHeroTitle = ev.BusinessHeroTitle,
                BusinessHeroSubtitle = ev.BusinessHeroSubtitle,
                BusinessCtaText = ev.BusinessCtaText,
                BusinessCard1Title = ev.BusinessCard1Title,
                BusinessCard1Body = ev.BusinessCard1Body,
                BusinessCard2Title = ev.BusinessCard2Title,
                BusinessCard2Body = ev.BusinessCard2Body,
                BusinessCard3Title = ev.BusinessCard3Title,
                BusinessCard3Body = ev.BusinessCard3Body,
                BusinessShareTitle = ev.BusinessShareTitle,
                MinimalHeroTitle = ev.MinimalHeroTitle,
                MinimalHeroSubtitle = ev.MinimalHeroSubtitle,
                MinimalCtaText = ev.MinimalCtaText,
                MinimalInfoTitle = ev.MinimalInfoTitle,
                MinimalInfoBody = ev.MinimalInfoBody,
                ColorfulHeroTitle = ev.ColorfulHeroTitle,
                ColorfulHeroSubtitle = ev.ColorfulHeroSubtitle,
                ColorfulCtaText = ev.ColorfulCtaText,
                ColorfulHighlight1 = ev.ColorfulHighlight1,
                ColorfulHighlight2 = ev.ColorfulHighlight2,
                ColorfulHighlight3 = ev.ColorfulHighlight3,
                AcademicHeroTitle = ev.AcademicHeroTitle,
                AcademicHeroSubtitle = ev.AcademicHeroSubtitle,
                AcademicCtaText = ev.AcademicCtaText,
                AcademicTrack1Title = ev.AcademicTrack1Title,
                AcademicTrack1Body = ev.AcademicTrack1Body,
                AcademicTrack2Title = ev.AcademicTrack2Title,
                AcademicTrack2Body = ev.AcademicTrack2Body,
                AcademicTrack3Title = ev.AcademicTrack3Title,
                AcademicTrack3Body = ev.AcademicTrack3Body,
                AcademicShareTitle = ev.AcademicShareTitle
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = HttpContext.Session.GetInt32("CurrentUserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var ev = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == model.Id);

            if (ev == null)
            {
                return NotFound();
            }

            if (ev.OwnerUserId != userId)
            {
                return Forbid();
            }

            ev.Name = model.Name;
            ev.Description = model.Description;
            ev.TemplateKey = model.TemplateKey;

            ev.TechProWhyAttendTitle = model.TechProWhyAttendTitle;
            ev.TechProWhyAttendPoint1 = model.TechProWhyAttendPoint1;
            ev.TechProWhyAttendPoint2 = model.TechProWhyAttendPoint2;
            ev.TechProWhyAttendPoint3 = model.TechProWhyAttendPoint3;
            ev.TechProCard1Title = model.TechProCard1Title;
            ev.TechProCard1Body = model.TechProCard1Body;
            ev.TechProCard2Title = model.TechProCard2Title;
            ev.TechProCard2Body = model.TechProCard2Body;
            ev.TechProCard3Title = model.TechProCard3Title;
            ev.TechProCard3Body = model.TechProCard3Body;

            ev.CreativeHeroTitle = model.CreativeHeroTitle;
            ev.CreativeHeroSubtitle = model.CreativeHeroSubtitle;
            ev.CreativeCtaText = model.CreativeCtaText;
            ev.CreativeShareTitle = model.CreativeShareTitle;

            ev.BusinessHeroBadge = model.BusinessHeroBadge;
            ev.BusinessHeroTitle = model.BusinessHeroTitle;
            ev.BusinessHeroSubtitle = model.BusinessHeroSubtitle;
            ev.BusinessCtaText = model.BusinessCtaText;
            ev.BusinessCard1Title = model.BusinessCard1Title;
            ev.BusinessCard1Body = model.BusinessCard1Body;
            ev.BusinessCard2Title = model.BusinessCard2Title;
            ev.BusinessCard2Body = model.BusinessCard2Body;
            ev.BusinessCard3Title = model.BusinessCard3Title;
            ev.BusinessCard3Body = model.BusinessCard3Body;
            ev.BusinessShareTitle = model.BusinessShareTitle;

            ev.MinimalHeroTitle = model.MinimalHeroTitle;
            ev.MinimalHeroSubtitle = model.MinimalHeroSubtitle;
            ev.MinimalCtaText = model.MinimalCtaText;
            ev.MinimalInfoTitle = model.MinimalInfoTitle;
            ev.MinimalInfoBody = model.MinimalInfoBody;

            ev.ColorfulHeroTitle = model.ColorfulHeroTitle;
            ev.ColorfulHeroSubtitle = model.ColorfulHeroSubtitle;
            ev.ColorfulCtaText = model.ColorfulCtaText;
            ev.ColorfulHighlight1 = model.ColorfulHighlight1;
            ev.ColorfulHighlight2 = model.ColorfulHighlight2;
            ev.ColorfulHighlight3 = model.ColorfulHighlight3;

            ev.AcademicHeroTitle = model.AcademicHeroTitle;
            ev.AcademicHeroSubtitle = model.AcademicHeroSubtitle;
            ev.AcademicCtaText = model.AcademicCtaText;
            ev.AcademicTrack1Title = model.AcademicTrack1Title;
            ev.AcademicTrack1Body = model.AcademicTrack1Body;
            ev.AcademicTrack2Title = model.AcademicTrack2Title;
            ev.AcademicTrack2Body = model.AcademicTrack2Body;
            ev.AcademicTrack3Title = model.AcademicTrack3Title;
            ev.AcademicTrack3Body = model.AcademicTrack3Body;
            ev.AcademicShareTitle = model.AcademicShareTitle;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { slug = ev.Slug });
        }

        [HttpGet]
        public IActionResult PreviewTemplate(string templateKey)
        {
            if (string.IsNullOrWhiteSpace(templateKey))
            {
                return NotFound();
            }

            var demoEvent = new Event
            {
                Id = 0,
                Name = templateKey switch
                {
                    "tech-pro" => "Tech Pro Conference",
                    "creative-spark" => "Creative Spark Festival",
                    "business-elite" => "Business Elite Summit",
                    "minimalist" => "Minimalist Focus Meetup",
                    "colorful" => "Colorful Experience Event",
                    "academic" => "Academic Research Conference",
                    _ => "Sample Event"
                },
                Description = templateKey switch
                {
                    "tech-pro" => "A conference for software, AI, and cloud leaders.",
                    "creative-spark" => "A vibrant event for designers, artists, and creators.",
                    "business-elite" => "An exclusive summit for executives and business leaders.",
                    "minimalist" => "A focused, distraction-free conference experience.",
                    "colorful" => "An energetic, colorful event full of experiences.",
                    "academic" => "An academic conference for researchers and practitioners.",
                    _ => "Sample description for this event template."
                },
                TemplateKey = templateKey,
                Slug = "preview-" + templateKey
            };

            ViewBag.IsOwner = false;

            return View("Details", demoEvent);
        }


        private async Task<string> GenerateUniqueSlug(string baseText)
        {
            string slug = baseText.Trim().ToLower()
                .Replace(" ", "-")
                .Replace("--", "-");

            if (string.IsNullOrWhiteSpace(slug))
            {
                slug = "event";
            }

            string finalSlug = slug;
            int counter = 1;

            while (await _context.Events.AnyAsync(e => e.Slug == finalSlug))
            {
                finalSlug = $"{slug}-{counter}";
                counter++;
            }

            return finalSlug;
        }
    }
}