using Microsoft.AspNetCore.Mvc;
using System;
using IBusinessLogic;
using Domain;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayableContentController : ControllerBase
    {
        private readonly IPlayerBL playerBL;

        public PlayableContentController(IPlayerBL playerBL)
        {
            this.playerBL = playerBL;
        }

        //GET: /api/playablecontent/{id}
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

        //POST: /api/playablecontent
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

        //DELETE: /api/playablecontent/{id}
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
