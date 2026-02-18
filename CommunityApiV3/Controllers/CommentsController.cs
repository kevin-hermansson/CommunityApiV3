using CommunityApiV3.Models;
using CommunityApiV3.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CommunityApiV3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        [HttpGet("{postId}")]
        [SwaggerOperation(Summary = "Hämta kommentarer för ett blogginlägg", Description = "Returnerar alla kommentarer som tillhör ett specifikt blogginlägg.")]
        [SwaggerResponse(200, "Lista med kommentarer returnerades")]
        public async Task<IActionResult> GetByPostId(int postId)
        {
            var comments = await _commentService.GetByPostIdAsync(postId);
            return Ok(comments);
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Skapa kommentar", Description = "Skapar en kommentar på ett blogginlägg om användaren inte är ägare av inlägget.")]
        [SwaggerResponse(200, "Kommentaren skapades")]
        [SwaggerResponse(404, "Användare eller inlägg hittades inte")]
        [SwaggerResponse(403, "Användaren får inte kommentera sitt eget inlägg")]
        public async Task<IActionResult> Create([FromBody] Comment comment)
        {
            var result = await _commentService.CreateAsync(comment);

            if (result == "NotFound")
                return NotFound("User or post not found");

            if (result == "Forbid")
                return StatusCode(403,"Cant make comment on your own post");


            return Ok("Comment created successfully");
        }


    }
}
