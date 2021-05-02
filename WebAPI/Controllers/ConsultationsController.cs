using Microsoft.AspNetCore.Mvc;
using System;
using IBusinessLogic;
using Domain;

namespace WebAPI.Controllers
{ 
	[ApiController]
	[Route("api/[Controller]")]
	public class ConsultationController : ControllerBase
	{
		private readonly IConsultationBL consultationBL;

		public ConsultationController(IConsultationBL consultationBL)
		{
			this.consultationBL = consultationBL;
		}

		[HttpGet("{id}")]
		public IActionResult GetConsultations()
		{
			var consultations = consultationBL.GetConsultations();
			return Ok(consultations);

		}

		[HttpGet("{id}")]
		public IActionResult GetConsultationsByPsychologist(int id)
		{
            try { 

				var consultations = consultationBL.GetConsultationsByPsychologist(id);
				return Ok(consultations);
			}
			catch (Exception e)
			{
				return NotFound(e.Message);
			}
		}

		[HttpGet("{id}")]
		public IActionResult GetConsultation(int id)
		{
            try { 
			var consultation = consultationBL.Get(id);
			return Ok(consultation);
			}
			catch (Exception e)
			{
				return NotFound(e.Message);
			}
		}

		[HttpPost]
		public IActionResult CreateConsultation([FromBody] Consultation consultation)
		{
			try { 
			consultationBL.CreateConsultation(consultation);
			return Created($"Consultation created at /api/consultation/{consultation.Id}", consultation);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}

}
