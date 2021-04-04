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
            PlayableContent content = playerBL.GetPlayableContent(id);
            return Ok(content);
        }
    }
}
