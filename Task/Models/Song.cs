using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task.Models
{
    public class Song
    {
        [Key]
        public int SongId { get; set; }

        [Required]
        [MaxLength(200)]
        public string SongTitle { get; set; } = null!;

        [MaxLength(200)]
        public string? Singer { get; set; }

        public ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();
    }
}
