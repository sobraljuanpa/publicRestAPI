using Microsoft.AspNetCore.Mvc;
using System;
using IBusinessLogic;
using Domain;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlayerBL playerBL;

        public PlaylistsController(IPlayerBL playerBL)
        {
            this.playerBL = playerBL;
        }

        [HttpGet]
        public IActionResult GetPlaylists()
        {
            try
            {
                List<Playlist> playlists = playerBL.GetPlaylists();
                return Ok(playlists);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPlaylistById(int id)
        {
            try
            {
                Playlist playlist = playerBL.GetPlaylist(id);
                return Ok(playlist);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public IActionResult CreatePlaylist([FromBody] Playlist playlist)
        {
            try
            {
                playerBL.AddPlaylist(playlist);
                return Created($"Playlist created at /api/playlist/{playlist.Id}", playlist);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{playlistId}/contents")]
        public IActionResult AddContentToPlaylist (int playlistId, [FromQuery] int contentId)
        {
            try 
            {
                Playlist playlist = playerBL.AddContentToPlaylist(playlistId,contentId);
                return Ok(playlist);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{playlistId}/contents")]
        public IActionResult RemoveContentFromPlaylist(int playlistId, [FromQuery] int contentId)
        {
            try
            {
                playerBL.DeleteContentFromPlaylist(playlistId, contentId);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{playlistId}/contents")]
        public IActionResult GetPlaylistContents(int playlistId)
        {
            try
            {
                List<PlayableContent> contents = playerBL.GetPlaylistContents(playlistId);
                return Ok(contents);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{playlistId}/videos")]
        public IActionResult AddVideoToPlaylist(int playlistId, [FromQuery] int videoId)
        {
            try
            {
                Playlist playlist = playerBL.AddVideoToPlaylist(playlistId, videoId);
                return Ok(playlist);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{playlistId}/videos")]
        public IActionResult RemoveVideoFromPlaylist(int playlistId, [FromQuery] int videoId)
        {
            try
            {
                playerBL.DeleteVideoFromPlaylist(playlistId, videoId);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{playlistId}/videos")]
        public IActionResult GetPlaylistVideos(int playlistId)
        {
            try
            {
                List<VideoContent> contents = playerBL.GetPlaylistVideos(playlistId);
                return Ok(contents);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlaylistById(int id)
        {
            try
            {
                playerBL.DeletePlaylist(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }
    }

}

