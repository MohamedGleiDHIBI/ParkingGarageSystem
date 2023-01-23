using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;

namespace ParkingGarageSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementsController : ControllerBase
    {
        private readonly IUserManagements _UserManagements;
        public UserManagementsController(IUserManagements userManagements)
        {
            _UserManagements = userManagements;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var existingUser = await _UserManagements.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                return BadRequest("User already exists");
            }

            var result = await _UserManagements.CreateUser(user);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _UserManagements.GetUserByEmail(loginModel.Email);
            if (user == null)
            {
                return BadRequest("Invalid login attempt.");
            }
            if (user.Password != loginModel.Password)
            {
                return BadRequest("Invalid login attempt.");
            }

            return Ok();
        }
    }
}
