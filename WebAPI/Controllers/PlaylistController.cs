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
    public class PlaylistController : ControllerBase
    {
        private readonly IPlayerBL playerBL;

        public PlaylistController(IPlayerBL playerBL)
        {
            this.playerBL = playerBL;
        }
    }

}

