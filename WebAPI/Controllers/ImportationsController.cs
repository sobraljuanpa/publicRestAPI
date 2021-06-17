using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using System;
using Microsoft.AspNetCore.Authorization;
using BusinessLogic;
using System.IO;
using System.Reflection;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ImportationsController : ControllerBase
    {
        private readonly IPlayerBL _playerBL;

        public ImportationsController(IPlayerBL playerBL)
        {
            _playerBL = playerBL;
        }

        [HttpPost("{type}")]
        public IActionResult ImportContent(string type, [FromBody] object[] parameters)
        {
            try
            {
                var dllFile = new FileInfo(@"..\BetterCalm.Importation.dll");
                Assembly assembly = Assembly.LoadFile(dllFile.FullName);

                Type importationType = assembly.GetType("BetterCalm.Importation" + "Importer" + type);
                IImportation importation = (IImportation)Activator.CreateInstance(importationType, parameters);
                IImportationBL logic = new BusinessLogic.Importation(importation,_playerBL);

                logic.AddPlayableContent();
                logic.AddVideoContent();
                logic.AddPlaylist();

                return Ok();
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }

        }
    }
}
