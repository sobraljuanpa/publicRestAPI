using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using IBusinessLogic;
using Domain;
using Domain.DTOs;
using System.Collections.Generic;

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
        public IActionResult AddPsychologist([FromBody] PsychologistDTO psychologist)
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

        [HttpGet]
        public IActionResult GetPsychologists()
        {
            try
            {
                List<PsychologistDTO> psychologists = psychologistBL.GetPsychologists();
                return Ok(psychologists);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPsychologistById(int id)
        {
            try
            {
                PsychologistDTO psychologist = psychologistBL.GetPsychologist(id);
                return Ok(psychologist);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }

        [HttpGet("schedules/{id}")]
        public IActionResult GetScheduleById(int id)
        {
            try
            {
                Schedule schedule = psychologistBL.GetSchedule(id);
                return Ok(schedule);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("{id}/problems")]
        public IActionResult AddProblemToPsychologist(int id, [FromBody] Psychologist psychologist)
        {
            try
            {
                psychologistBL.AddProblemToPsychologist(psychologist, id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("schedules")]
        public IActionResult AddSchedule([FromBody] Schedule schedule)
        {
            try
            {
                psychologistBL.AddSchedule(schedule);
                return Created(" ", schedule);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{id}/schedules")]
        public IActionResult AddScheduleToPsychologist(int id, [FromBody] Psychologist psychologist)
        {
            try
            {
                psychologistBL.AddScheduleToPsychologist(psychologist, id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePsychologist (int id, [FromBody] PsychologistDTO psychologist)
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
