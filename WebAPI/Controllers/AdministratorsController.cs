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
    public class AdministratorsController : ControllerBase
    {
        private readonly IAdministratorBL administratorBL;

        public AdministratorsController(IAdministratorBL administratorBL)
        {
            this.administratorBL = administratorBL;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Administrator admin)
        {

            Administrator auxAdmin = administratorBL.Authenticate(admin.Email, admin.Password);
            if(auxAdmin != null)
            {
                return Accepted(admin);
            }
            return Unauthorized(new { message = "Username or password incorrect" });
        }

        [HttpGet("{id}")]
        public IActionResult GetAdministratorById (int id)
        {
            try
            {
                Administrator administrator = administratorBL.Get(id);
                return Ok(administrator);
            } 
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }

        [HttpPost()]
        public IActionResult AddAdministrator (Administrator administrator)
        {
            try
            {
                administratorBL.AddAdministrator(administrator);
                return Created(" ",administrator);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAdministrator (int id)
        {
            try
            {
                administratorBL.DeleteAdministrator(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("{id}")]
        public IActionResult UpdateAdministrator (int id, Administrator admin)
        {
            try
            {
                administratorBL.UpdateAdministrator(id, admin);
                return Ok(admin);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

    }
}
