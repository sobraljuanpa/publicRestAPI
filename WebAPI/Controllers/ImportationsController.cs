using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using System;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Reflection;
using BusinessLogic;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ImportationsController : ControllerBase
    {
        private readonly IImportationLogic _importationLogic;
        private readonly PlayerBL _playerBL;

        public ImportationsController(IImportationLogic importationLogic, PlayerBL playerBL)
        {
            _importationLogic = importationLogic;
            _playerBL = playerBL;
        }

        [HttpPost("{type}")]
        public IActionResult ImportContent(string type, [FromBody] object[] parameters)
        {
            try
            {
                _importationLogic.LoadFile(type, parameters);
                return Ok();
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }

        }
    }
}
