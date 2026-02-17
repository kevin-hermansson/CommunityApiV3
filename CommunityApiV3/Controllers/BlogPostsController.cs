using CommunityApiV3.Models;
using CommunityApiV3.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Hämta alla blogginlägg", Description = "Returnerar en lista med alla blogginlägg")]
        [SwaggerResponse(200, "Lista med alla blogginlägg returnerades.")]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _blogPostService.GetAllAsync();
            return Ok(posts);
        }


        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Hämta blogginlägg via id", Description = "Returnerar ett inlägg baserat på valt id.")]
        [SwaggerResponse(200, "Blogginlägg hittades")]
        [SwaggerResponse(404, "Blogginlägg kunde inte hittas")]
        public async Task<IActionResult> GetById(int id)
        {
            var post = await _blogPostService.GetByIdAsync(id);
            if (post == null)
                return NotFound("Blogpost not found");

            return Ok(post);
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Skapa nytt blogginlägg", Description = "Skapar nytt blogginlägg kopplat till en användare och kategori")]
        [SwaggerResponse(200, "Blogginlägget skapades")]
        [SwaggerResponse(400, "Ogiltig användare eller kategori")]
        public async Task<IActionResult> Create([FromBody] BlogPost post)
        {
            var result = await _blogPostService.CreateAsync(post);

            if (!result)
                return BadRequest("Invalid user or category");

            return Ok("Blogpost created");
        }


        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Uppdatera blogginlägg", Description = "Uppdaterar ett blogginlägg om rätt användare försöker uppdatera det.")]
        [SwaggerResponse(200, "Blogginlägget uppdaterades")]
        [SwaggerResponse(404, "Blogginlägget hittades inte")]
        [SwaggerResponse(403, "Användaren har inte rätt att uppdatera detta inlägg")]
        public async Task<IActionResult> Update(int id, [FromBody] BlogPost updatedPost)
        {
            var result = await _blogPostService.UpdateAsync(id, updatedPost.UserId, updatedPost);

            if (result == "NotFound")
                return NotFound("Post not found");

            if (result == "Forbid")
                return Forbid();

            return Ok("Blogpost updated successfully");
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Ta bort blogginlägg", Description = "Tar bort ett blogginlägg om rätt användare försöker ta bort det.")]
        [SwaggerResponse(200, "Blogginlägget togs bort")]
        [SwaggerResponse(404, "Blogginlägget hittades inte")]
        [SwaggerResponse(403, "Användaren har inte rätt att ta bort detta inlägg")]
        public async Task<IActionResult> Delete(int id, [FromBody] BlogPost post)
        {
            var result = await _blogPostService.DeleteAsync(id, post.UserId);

            if (result == "NotFound")
                return NotFound("Post not found");

            if (result == "Forbidden")
                return Forbid();

            return Ok("Blogpost deleted successfully");
        }


        [HttpGet("search")]
        [SwaggerOperation(Summary = "Sök blogginlägg via titel", Description = "Returnerar de blogginlägg som innehåller sökordet")]
        [SwaggerResponse(200,"Lista med matchande blogginlägg returnerades")]
        public async Task<IActionResult> SearchByTitle (string title)
        {
            var posts = await _blogPostService.SearchByTitleAsync(title);
            return Ok(posts);
        }

        [HttpGet("category/{categoryId}")]
        [SwaggerOperation(Summary = "Hämta blogginlägg via kategori", Description = "Returnerar alla blogginlägg som tillhör vald kategori")]
        [SwaggerResponse(200, "Lista med blogginlägg returnerades")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var posts = await _blogPostService.GetByCategoryAsync(categoryId);
            return Ok(posts);
        }


    }
}
