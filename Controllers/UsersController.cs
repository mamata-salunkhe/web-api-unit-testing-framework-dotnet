using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> CheckUser(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email required");

            bool exists = await _userService.UserExists(email);

            if (!exists)
                return NotFound("User not found");

            return Ok("User exists");
        }
        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            if (id <= 0)
                return BadRequest();

            var product = await _userService.GetById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            var createdUser = _userService.CreateUser(user);
            return CreatedAtAction(nameof(Create), createdUser);
        }
    }
}
