using System.Linq;
using System.Threading.Tasks;
using BuildMyEvent.Data;
using BuildMyEvent.Models;
using BuildMyEvent.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildMyEvent.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string SessionUserIdKey = "CurrentUserId";

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.Session.GetInt32(SessionUserIdKey);
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.Value);
            if (user == null || !user.IsActive)
            {
                HttpContext.Session.Remove(SessionUserIdKey);
                return RedirectToAction("Login");
            }

            var totalEvents = await _context.Events
                .CountAsync(e => e.OwnerUserId == user.Id);

            var totalRegistrations = await _context.Registrations
                .Include(r => r.Event)
                .CountAsync(r => r.Event != null && r.Event.OwnerUserId == user.Id);

            ViewBag.TotalEvents = totalEvents;
            ViewBag.TotalRegistrations = totalRegistrations;

            var model = new EditProfileViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(EditProfileViewModel model, string submit)
        {
            var userId = HttpContext.Session.GetInt32(SessionUserIdKey);
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            if (submit == "cancel")
            {
                return RedirectToAction("Profile");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.Value);
            if (user == null || !user.IsActive)
            {
                HttpContext.Session.Remove(SessionUserIdKey);
                return RedirectToAction("Login");
            }

            user.Name = model.Name;
            user.Email = model.Email;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Your profile has been updated.";
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existing = await _context.Users.AnyAsync(u => u.Email == model.Email);
            if (existing)
            {
                ModelState.AddModelError(nameof(model.Email), "This email is already registered.");
                return View(model);
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetInt32(SessionUserIdKey, user.Id);

            TempData["SuccessMessage"] = "Your account has been created and you are now logged in.";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }

            if (!user.IsActive)
            {
                ModelState.AddModelError(string.Empty, "Your account is deactivated. Please contact support.");
                return View(model);
            }

            HttpContext.Session.SetInt32(SessionUserIdKey, user.Id);

            TempData["SuccessMessage"] = "You have logged in successfully.";

            if (user.IsAdmin)
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionUserIdKey);
            TempData["SuccessMessage"] = "You have been logged out.";
            return RedirectToAction("Index", "Home");
        }

        public int? GetCurrentUserId()
        {
            return HttpContext.Session.GetInt32(SessionUserIdKey);
        }
    }
}
