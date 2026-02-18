using Microsoft.AspNetCore.Mvc;
using CommunityApiV3.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using CommunityApiV3.DTOs.Categories;

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
        [SwaggerOperation(Summary = "Hämta alla kategorier", Description = "Returnerar en lista med alla kategorier som finns sparade i databasen.")]
        [SwaggerResponse(200, "Lista med kategorier returnerades")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Hämta kategori via id", Description = "Returnerar en specifik kategori baserat på angivet id.")]
        [SwaggerResponse(200, "Kategori hittad")]
        [SwaggerResponse(404, "Kategori hittades inte")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Skapa ny kategori", Description = "Skapar en ny kategori och sparar den i databasen.")]
        [SwaggerResponse(201, "Kategori skapad")]
        [SwaggerResponse(400, "Felaktig inmatning ")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            var id = await _categoryService.CreateAsync(dto);
            return Created("", id);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Ta bort kategori", Description = "Tar bort en kategori från databasen baserat på angivet id.")]
        [SwaggerResponse(200, "Kategori borttagen")]
        [SwaggerResponse(404, "Kategori hittades inte")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok("Category deleted");
        }
    }
}
