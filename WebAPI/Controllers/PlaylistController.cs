using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using IBusinessLogic;
using Domain;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlayerBL playerBL;

        public PlaylistController(IPlayerBL playerBL)
        {
            this.playerBL = playerBL;
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

