using System.Collections.Generic;
using BuildMyEvent.Models;

namespace BuildMyEvent.Models.ViewModels
{
    public class AdminRegistrationsViewModel
    {
        public List<Event> Events { get; set; } = new List<Event>();

        public int? SelectedEventId { get; set; }

        public List<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
