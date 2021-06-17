using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using System;
using Microsoft.AspNetCore.Authorization;
using BusinessLogic;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ImportationsController : ControllerBase
    {
        private readonly IImportationBL _importationLogic;

        public ImportationsController(IImportationBL importationLogic)
        {
            _importationLogic = importationLogic;
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
