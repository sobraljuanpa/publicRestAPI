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

        [HttpPost("{importerPath}")]
        public IActionResult ImportContent(string importerPath, [FromBody] object[] param)
        {
            try
            {
                var dllFile = new FileInfo(importerPath);
                Assembly assembly = Assembly.LoadFile(dllFile.FullName);

                foreach (Type type in assembly.GetTypes())
                {
                    IImportation importation = (IImportation)Activator.CreateInstance(type,param);
                    IImportationLogic logic = new BusinessLogic.Importation(importation,_playerBL);
                    logic.AddPlayableContent();
                    logic.AddPlaylist();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }

        }
    }
}
