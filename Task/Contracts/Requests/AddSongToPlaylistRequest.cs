using System.ComponentModel.DataAnnotations;

namespace Task.Contracts.Requests
{
    public sealed class AddSongToPlaylistRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int SongId { get; set; }
    }
}
