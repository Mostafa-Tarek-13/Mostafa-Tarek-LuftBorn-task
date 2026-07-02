using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Email { get; set; } = null!;

        public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
