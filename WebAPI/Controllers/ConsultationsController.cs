using Microsoft.AspNetCore.Mvc;
using System;
using IBusinessLogic;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[Controller]")]
	public class ConsultationsController : ControllerBase
	{
		private readonly IConsultationBL consultationBL;

		public ConsultationsController(IConsultationBL consultationBL)
		{
			this.consultationBL = consultationBL;
		}

		[HttpGet]
		public IActionResult GetConsultations()
		{
			var consultations = consultationBL.GetConsultations();

			return Ok(consultations);
		}

        [HttpGet("psychologist/{id}")]
        public IActionResult GetConsultationsByPsychologist(int id)
        {
            try
            {
                var consultations = consultationBL.GetConsultationsByPsychologist(id);
                return Ok(consultations);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

		[AllowAnonymous]
        [HttpGet("{id}")]
		public IActionResult GetConsultation(int id)
		{
            try 
			{ 
				var consultation = consultationBL.Get(id);
				return Ok(consultation);
			}
			catch (Exception e)
			{
				return NotFound(e.Message);
			}
		}

		[HttpPost]
		public IActionResult CreateConsultation([FromBody] ConsultationDTO consultation)
		{
			try 
			{ 
				consultationBL.CreateConsultation(consultation);
				return Created($"Consultation created at /api/consultation/{consultation.Id}", 
							   consultation);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}

}
