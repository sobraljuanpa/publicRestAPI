using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using IBusinessLogic;
using Domain;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PsychologistsController : ControllerBase
    {
        private readonly IPsychologistBL psychologistBL;

        public PsychologistsController(IPsychologistBL psychologistBL)
        {
            this.psychologistBL = psychologistBL;
        }

        [HttpPost()]
        public IActionResult AddPsychologist([FromBody] Psychologist psychologist)
        {
            try
            {
                psychologistBL.AddPsychologist(psychologist);
                return Created(" ", psychologist);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePsychologist(int id)
        {
            try
            {
                psychologistBL.DeletePsychologist(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPsychologistById (int id)
        {
            try
            {
                Psychologist psychologist = psychologistBL.GetPsychologist(id);
                return Ok(psychologist);
            } 
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }

        [HttpPost("{id}")]
        public IActionResult UpdatePsychologist (int id, [FromBody] Psychologist psychologist)
        {
            try
            {
                psychologistBL.UpdatePsychologist(id, psychologist);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

    }
}
