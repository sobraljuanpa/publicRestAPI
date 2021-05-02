using Microsoft.AspNetCore.Mvc;
using System;
using IBusinessLogic;
using Domain;

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
