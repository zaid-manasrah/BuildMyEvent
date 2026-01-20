namespace BuildMyEvent.Models.ViewModels
{
    public class AdminUserListItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }

        public int EventsCount { get; set; }
    }
}
