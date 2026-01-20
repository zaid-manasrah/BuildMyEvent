using System;
using System.ComponentModel.DataAnnotations;

namespace BuildMyEvent.Models
{
    public class Registration
    {
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }
        public Event? Event { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Phone { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}
