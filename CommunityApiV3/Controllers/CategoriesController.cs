using CommunityApiV3.Models;
using CommunityApiV3.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CommunityApiV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Hämta kategorier", Description ="Returnerar en lista med alla kategorier")]
        [SwaggerResponse(200,"Lista med kategorier returnerades")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Skapa ny kategori", Description ="Skapar en ny kategori i databasen")]
        [SwaggerResponse(200, "Kategorin skapades")]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            await _categoryService.CreateAsync(category);
            return Ok("Category created succesfully");
        }




    }
}
