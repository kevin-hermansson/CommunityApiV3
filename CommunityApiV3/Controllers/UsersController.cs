using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CommunityApiV3.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using CommunityApiV3.Models;
using CommunityApiV3.DTOs;
using CommunityApiV3.DTOs.Users;
namespace CommunityApiV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UsersController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }


        [HttpGet]
        [SwaggerOperation(Summary ="Hämta alla användare", Description ="Returnerar en lista med alla registrerade användare.")]
        [SwaggerResponse(200,"Lista med användare returnerades")]
        public async Task <IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }


        [HttpPost("register")]
        [SwaggerOperation(Summary = "Registrera ny användare", Description = "Skapar nytt användarkonto.")]
        [SwaggerResponse(200,"Användare skapad och returnerar dess id")]
        [SwaggerResponse(400,"Användarnamnet är upptaget")]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            var newUserId = await _userService.CreateAsync(user);

            if (newUserId == 0)
                return BadRequest("Username already taken ");

            return Ok(newUserId);
        }


        [HttpPost("login")]
        [SwaggerOperation(Summary = "Logga in användare", Description = "Verifierar username och password och returnerar en JWT-token.")]
        [SwaggerResponse(200, "Inloggning lyckades och token returneras")]
        [SwaggerResponse(401, "Fel användarnamn eller lösenord")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var userId = await _userService.LoginAsync(loginDto.Username, loginDto.Password);

            if (userId == null)
                return Unauthorized("Invalid username or password");

            var token = _tokenService.CreateToken(userId.Value, loginDto.Username);

            return Ok(new TokenDto { Token = token });
        }



        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Hämta användare via id", Description = "Returnerar den användare med valt id.")]
        [SwaggerResponse(200,"Användare hittad")]
        [SwaggerResponse(404,"Användare kunde inte hittas")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Uppdatera en användare", Description ="Uppdatera username/password/email för en användare.")]
        [SwaggerResponse(200,"Användare uppdaterad")]
        [SwaggerResponse(404,"Användare kunde inte hittas")]
        public async Task<IActionResult> Update(int id, [FromBody] User updatedUser)
        {
            var result = await _userService.UpdateAsync(id, updatedUser);

            if (!result)
                return NotFound("User not found");

            return Ok("User was updated");
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Ta bort användare", Description ="Tar bort användare via id.")]
        [SwaggerResponse(200,"Användare togs bort")]
        [SwaggerResponse(404,"Användare kunde inte hittas")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteAsync(id);

            if (!result)
                return NotFound("User not found");

            return Ok("User was deleted");
        }



        
    }
}
