using Microsoft.AspNetCore.Mvc;
using IBusinessLogic;
using System;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IPlayerBL playerBL;

        public CategoriesController(IPlayerBL playerBL)
        {
            this.playerBL = playerBL;
        }

        [HttpGet()]
        public IActionResult GetCategories()
        {
            try
            {
                var categories = playerBL.GetCategories();
                return Ok(categories);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryContents(int id)
        {
            try
            {
                var contents = playerBL.GetCategoryElements(id);
                return Ok(contents);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

    }
}
