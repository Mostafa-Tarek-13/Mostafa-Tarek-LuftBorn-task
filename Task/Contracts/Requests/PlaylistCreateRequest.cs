using System.ComponentModel.DataAnnotations;

namespace Task.Contracts.Requests
{
    public sealed class PlaylistCreateRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Title { get; set; } = null!;
    }
}
