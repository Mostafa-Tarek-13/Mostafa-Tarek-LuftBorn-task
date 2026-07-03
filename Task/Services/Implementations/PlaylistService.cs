using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.Common.Exceptions;
using Task.Contracts.Requests;
using Task.Contracts.Responses;
using Task.Models;
using Task.Repositories.Interfaces;
using Task.Services.Interfaces;

namespace Task.Services.Implementations
{
    public sealed class PlaylistService : IPlaylistService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlaylistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async System.Threading.Tasks.Task<PlaylistResponse> CreatePlaylistAsync(PlaylistCreateRequest request)
        {
            if (request == null)
            {
                throw new InvalidRequestException("Playlist create request cannot be null.");
            }

            if (!await _unitOfWork.Users.ExistsAsync(request.UserId))
            {
                throw new EntityNotFoundException($"User with id {request.UserId} was not found.");
            }

            var playlist = new Playlist
            {
                Title = request.Title.Trim(),
                UserId = request.UserId,
                CreationDate = DateTime.UtcNow
            };

            await _unitOfWork.Playlists.AddAsync(playlist);
            await _unitOfWork.SaveChangesAsync();

            return MapToResponse(playlist);
        }

        public async System.Threading.Tasks.Task<IEnumerable<PlaylistResponse>> GetPlaylistsByUserAsync(int userId)
        {
            if (!await _unitOfWork.Users.ExistsAsync(userId))
            {
                throw new EntityNotFoundException($"User with id {userId} was not found.");
            }

            var playlists = await _unitOfWork.Playlists.GetByUserIdAsync(userId);
            return playlists.Select(MapToResponse);
        }

        public async System.Threading.Tasks.Task<PlaylistSongResponse> AddSongToPlaylistAsync(int playlistId, AddSongToPlaylistRequest request)
        {
            if (request == null)
            {
                throw new InvalidRequestException("Request body cannot be null.");
            }

            await ValidatePlaylistOwnershipAsync(playlistId, request.UserId);
            await ValidateSongExistsAsync(request.SongId);

            if (await _unitOfWork.PlaylistSongs.ExistsAsync(playlistId, request.SongId))
            {
                throw new InvalidRequestException("The song is already added to the playlist.");
            }

            var playlistSong = new PlaylistSong
            {
                PlaylistId = playlistId,
                SongId = request.SongId
            };

            await _unitOfWork.PlaylistSongs.AddAsync(playlistSong);
            await _unitOfWork.SaveChangesAsync();

            return new PlaylistSongResponse
            {
                PlaylistId = playlistId,
                SongId = request.SongId
            };
        }

        public async System.Threading.Tasks.Task<PlaylistSongResponse> UpdatePlaylistSongAsync(int playlistId, int songId, UpdatePlaylistSongRequest request)
        {
            if (request == null)
            {
                throw new InvalidRequestException("Request body cannot be null.");
            }

            await ValidatePlaylistOwnershipAsync(playlistId, request.UserId);

            var existingPlaylistSong = await _unitOfWork.PlaylistSongs.GetAsync(playlistId, songId);
            if (existingPlaylistSong == null)
            {
                throw new EntityNotFoundException($"Song with id {songId} is not associated with playlist {playlistId}.");
            }

            if (!await _unitOfWork.Songs.ExistsAsync(request.NewSongId))
            {
                throw new EntityNotFoundException($"Song with id {request.NewSongId} was not found.");
            }

            if (await _unitOfWork.PlaylistSongs.ExistsAsync(playlistId, request.NewSongId))
            {
                throw new InvalidRequestException("The new song is already added to the playlist.");
            }

            existingPlaylistSong.SongId = request.NewSongId;
            await _unitOfWork.SaveChangesAsync();

            return new PlaylistSongResponse
            {
                PlaylistId = playlistId,
                SongId = request.NewSongId
            };
        }

        public async System.Threading.Tasks.Task DeleteSongFromPlaylistAsync(int playlistId, int songId)
        {
            var playlist = await _unitOfWork.Playlists.GetByIdAsync(playlistId);
            if (playlist == null)
            {
                throw new EntityNotFoundException($"Playlist with id {playlistId} was not found.");
            }

            var playlistSong = await _unitOfWork.PlaylistSongs.GetAsync(playlistId, songId);
            if (playlistSong == null)
            {
                throw new EntityNotFoundException($"Song with id {songId} is not associated with playlist {playlistId}.");
            }

            _unitOfWork.PlaylistSongs.Remove(playlistSong);
            await _unitOfWork.SaveChangesAsync();
        }

        private async System.Threading.Tasks.Task ValidatePlaylistOwnershipAsync(int playlistId, int userId)
        {
            if (!await _unitOfWork.Users.ExistsAsync(userId))
            {
                throw new EntityNotFoundException($"User with id {userId} was not found.");
            }

            var playlist = await _unitOfWork.Playlists.GetByIdAsync(playlistId);
            if (playlist == null)
            {
                throw new EntityNotFoundException($"Playlist with id {playlistId} was not found.");
            }

            if (playlist.UserId != userId)
            {
                throw new InvalidRequestException("The playlist does not belong to the specified user.");
            }
        }

        private async System.Threading.Tasks.Task ValidateSongExistsAsync(int songId)
        {
            if (!await _unitOfWork.Songs.ExistsAsync(songId))
            {
                throw new EntityNotFoundException($"Song with id {songId} was not found.");
            }
        }

        private static PlaylistResponse MapToResponse(Playlist playlist)
        {
            return new PlaylistResponse
            {
                PlaylistId = playlist.PlaylistId,
                UserId = playlist.UserId,
                Title = playlist.Title,
                CreationDate = playlist.CreationDate
            };
        }
    }
}
