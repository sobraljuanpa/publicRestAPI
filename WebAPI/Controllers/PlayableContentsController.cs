using Microsoft.AspNetCore.Mvc;
using System;
using IBusinessLogic;
using Domain;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayableContentsController : ControllerBase
    {
        private readonly IPlayerBL playerBL;

        public PlayableContentsController(IPlayerBL playerBL)
        {
            this.playerBL = playerBL;
        }

        [HttpGet]
        public IActionResult GetAllContents()
        {
            try
            {
                List<PlayableContent> contents = playerBL.GetContents();
                return Ok(contents);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetContentById(int id)
        {
            try
            {
                PlayableContent content = playerBL.GetPlayableContent(id);
                return Ok(content);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateContent([FromBody] PlayableContent content)
        {
            try
            {
                playerBL.AddIndependentContent(content);
                return Created($"Content created at /api/playablecontent/{content.Id}", content);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("videos")]
        public IActionResult CreateVideo([FromBody] VideoContent video)
        {
            try
            {
                playerBL.AddVideoContent(video);
                return Created($"Content created at /api/playablecontent/{video.Id}", video);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("videos")]
        public IActionResult GetAllVideos()
        {
            try
            {
                List<VideoContent> videos = playerBL.GetVideos();
                return Ok(videos);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("videos/{id}")]
        public IActionResult GetVideoById(int id)
        {
            try
            {
                VideoContent video = playerBL.GetVideo(id);
                return Ok(video);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("videos/{id}")]
        public IActionResult DeleteVideoById(int id)
        {
            try
            {
                playerBL.DeleteVideo(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContentById(int id)
        {
            try
            {
                playerBL.DeleteContent(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
