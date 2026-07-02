using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task.Models
{
    public class Playlist
    {
        [Key]
        public int PlaylistId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public DateTime CreationDate { get; set; }

        public ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();
    }
}
