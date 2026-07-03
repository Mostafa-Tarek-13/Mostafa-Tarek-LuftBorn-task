using Microsoft.AspNetCore.Mvc;
using Task.Common.Exceptions;
using Task.Contracts.Requests;
using Task.Contracts.Responses;
using Task.Services.Interfaces;

namespace Task.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;

        public PlaylistsController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlaylist([FromBody] PlaylistCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var playlist = await _playlistService.CreatePlaylistAsync(request);
                return CreatedAtAction(nameof(GetPlaylistsByUser), new { userId = playlist.UserId }, playlist);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (InvalidRequestException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetPlaylistsByUser(int userId)
        {
            try
            {
                var playlists = await _playlistService.GetPlaylistsByUserAsync(userId);
                return Ok(playlists);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
        }

        [HttpPost("{playlistId:int}/songs")]
        public async Task<IActionResult> AddSongToPlaylist(int playlistId, [FromBody] AddSongToPlaylistRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _playlistService.AddSongToPlaylistAsync(playlistId, request);
                return CreatedAtAction(nameof(GetPlaylistsByUser), new { userId = request.UserId }, result);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (InvalidRequestException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPut("{playlistId:int}/songs/{songId:int}")]
        public async Task<IActionResult> UpdateSongInPlaylist(int playlistId, int songId, [FromBody] UpdatePlaylistSongRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updated = await _playlistService.UpdatePlaylistSongAsync(playlistId, songId, request);
                return Ok(updated);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (InvalidRequestException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete("{playlistId:int}/songs/{songId:int}")]
        public async Task<IActionResult> DeleteSongFromPlaylist(int playlistId, int songId)
        {
            try
            {
                await _playlistService.DeleteSongFromPlaylistAsync(playlistId, songId);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (InvalidRequestException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
