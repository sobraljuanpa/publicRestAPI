using Microsoft.AspNetCore.Mvc;
using System;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Filters;

namespace WebAPI.Controllers
{

    [ApiController]
    [ExceptionFilter]
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

            var categories = playerBL.GetCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryContents(int id)
        {

            var contents = playerBL.GetCategoryElements(id);
            return Ok(contents);
        }

    }
}
