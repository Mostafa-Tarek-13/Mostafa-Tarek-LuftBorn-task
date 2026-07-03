namespace Task.Contracts.Responses
{
    public sealed class PlaylistResponse
    {
        public int PlaylistId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime CreationDate { get; set; }
    }
}
