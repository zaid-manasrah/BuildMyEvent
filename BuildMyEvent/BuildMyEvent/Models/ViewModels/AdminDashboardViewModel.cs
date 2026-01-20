using System.Collections.Generic;
using BuildMyEvent.Models;

namespace BuildMyEvent.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalEvents { get; set; }
        public int TotalRegistrations { get; set; }

        public List<TemplateUsageViewModel> TopTemplates { get; set; } = new();
        public List<Event> LatestEvents { get; set; } = new();
        public List<Registration> LatestRegistrations { get; set; } = new();
    }

    public class TemplateUsageViewModel
    {
        public string TemplateKey { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
