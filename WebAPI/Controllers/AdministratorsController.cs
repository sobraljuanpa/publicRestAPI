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
    public class AdministratorsController : ControllerBase
    {
        private readonly IAdministratorBL administratorBL;

        public AdministratorsController(IAdministratorBL administratorBL)
        {
            this.administratorBL = administratorBL;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Administrator admin)
        {

            bool aux = administratorBL.Authenticate(admin.Email, admin.Password);
            if(aux)
            {
                return Ok("Authentication successful");
            }
            return Unauthorized(new { message = "Username or password incorrect" });
        }
    }
}
