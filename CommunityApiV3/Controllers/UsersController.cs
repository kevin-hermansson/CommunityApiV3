using Microsoft.AspNetCore.Mvc;
using CommunityApiV3.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using CommunityApiV3.DTOs.Users;

namespace CommunityApiV3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Hämta alla användare", Description = "Returnerar en lista med alla registrerade användare.")]
        [SwaggerResponse(200, "Lista med användare returnerades")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }



        [HttpPost("register")]
        [SwaggerOperation(Summary = "Registrera ny användare", Description = "Skapar ett nytt användarkonto och returnerar användarens id.")]
        [SwaggerResponse(201, "Användare skapad och id returneras")]
        [SwaggerResponse(400, "Användarnamnet är redan upptaget")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
        {
            var newUserId = await _userService.CreateAsync(dto);

            if (newUserId == 0)
                return BadRequest("Username already taken");

            return Created("",newUserId);
        }



        [HttpPost("login")]
        [SwaggerOperation(Summary = "Logga in användare", Description = "Verifierar username och password och returnerar användarens id.")]
        [SwaggerResponse(200, "Inloggning lyckades och userId returneras")]
        [SwaggerResponse(401, "Fel användarnamn eller lösenord")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var userId = await _userService.LoginAsync(dto);

            if (userId == null)
                return Unauthorized("Invalid username or password");

            return Ok(userId);
        }



        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Hämta användare via id", Description = "Returnerar en specifik användare baserat på angivet id.")]
        [SwaggerResponse(200, "Användare hittad")]
        [SwaggerResponse(404, "Användare hittades inte")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }



        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Uppdatera användare", Description = "Uppdaterar username, password eller email för en användare.")]
        [SwaggerResponse(200, "Användare uppdaterad")]
        [SwaggerResponse(404, "Användare hittades inte")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            var result = await _userService.UpdateAsync(id, dto);

            if (!result)
                return NotFound("User not found");

            return Ok("User updated");
        }



        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Ta bort användare", Description = "Tar bort en användare från databasen.")]
        [SwaggerResponse(200, "Användare togs bort")]
        [SwaggerResponse(404, "Användare hittades inte")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteAsync(id);

            if (!result)
                return NotFound("User not found");

            return Ok("User deleted");
        }
    }
}

