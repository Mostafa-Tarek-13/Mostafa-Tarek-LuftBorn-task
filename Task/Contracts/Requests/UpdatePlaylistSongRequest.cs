using System.ComponentModel.DataAnnotations;

namespace Task.Contracts.Requests
{
    public sealed class UpdatePlaylistSongRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int NewSongId { get; set; }
    }
}
