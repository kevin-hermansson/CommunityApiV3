using Microsoft.AspNetCore.Mvc;
using CommunityApiV3.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using CommunityApiV3.DTOs.BlogPosts;

namespace CommunityApiV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostsController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Hämta alla blogginlägg", Description = "Returnerar en lista med alla blogginlägg.")]
        [SwaggerResponse(200, "Lista med blogginlägg returnerades")]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _blogPostService.GetAllAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Hämta blogginlägg via id", Description = "Returnerar ett specifikt blogginlägg baserat på angivet id.")]
        [SwaggerResponse(200, "Blogginlägg hittat")]
        [SwaggerResponse(404, "Blogginlägg hittades inte")]
        public async Task<IActionResult> GetById(int id)
        {
            var post = await _blogPostService.GetByIdAsync(id);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpGet("title/{title}")]
        [SwaggerOperation(Summary = "Sök blogginlägg på titel", Description = "Returnerar blogginlägg vars titel matchar angivet värde.")]
        [SwaggerResponse(200, "Sökresultat returnerades")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var posts = await _blogPostService.GetByTitleAsync(title);
            return Ok(posts);
        }

        [HttpGet("category/{categoryId}")]
        [SwaggerOperation(Summary = "Sök blogginlägg på kategori", Description = "Returnerar blogginlägg som tillhör angiven kategori.")]
        [SwaggerResponse(200, "Sökresultat returnerades")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var posts = await _blogPostService.GetByCategoryAsync(categoryId);
            return Ok(posts);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Skapa nytt blogginlägg", Description = "Skapar ett nytt blogginlägg kopplat till en användare och kategori.")]
        [SwaggerResponse(201, "Blogginlägg skapat")]
        [SwaggerResponse(400, "Ogiltig användare eller kategori")]
        public async Task<IActionResult> Create([FromBody] CreateBlogPostDto dto)
        {
            var id = await _blogPostService.CreateAsync(dto);

            if (id == null)
                return BadRequest("Invalid user or category");

            return Created("", id);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Uppdatera blogginlägg", Description = "Uppdaterar ett blogginlägg. Endast skaparen får uppdatera.")]
        [SwaggerResponse(200, "Blogginlägg uppdaterat")]
        [SwaggerResponse(400, "Inte behörig eller ogiltigt inlägg")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBlogPostDto dto)
        {
            var result = await _blogPostService.UpdateAsync(id, dto);

            if (!result)
                return BadRequest("Not allowed or blogpost not found");

            return Ok("Blogpost updated");
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Ta bort blogginlägg", Description = "Tar bort ett blogginlägg. Endast ägaren får ta bort.")]
        [SwaggerResponse(200, "Blogginlägg borttaget")]
        [SwaggerResponse(400, "Inte behörig eller ogiltigt inlägg")]
        public async Task<IActionResult> Delete(int id, [FromBody] DeleteBlogPostDto dto)
        {
            var result = await _blogPostService.DeleteAsync(id, dto.UserId);

            if (!result)
                return BadRequest("Not allowed or blogpost not found");

            return Ok("Blogpost deleted");
        }
    }
}
