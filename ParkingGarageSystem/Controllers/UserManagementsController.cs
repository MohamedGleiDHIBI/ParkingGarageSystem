using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpGet]
        [Route("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = int.Parse(User.Identity.Name);
            var user = await _UserManagements.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [Authorize]
        [HttpPut]
        [Route("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var userId = int.Parse(User.Identity.Name);
            var user = await _UserManagements.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Password != model.CurrentPassword)
            {
                return BadRequest("Invalid current password.");
            }

            var result = await _UserManagements.UpdateUser(user);
            if (result)
            {
                return Ok();
            }
            return BadRequest("An error occurred while changing the password.");
        }
        [Authorize]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteUser()
        {
            var userId = int.Parse(User.Identity.Name);
            var result = await _UserManagements.DeleteUser(userId);
            if (result)
            {
                return Ok();
            }
            return BadRequest("An error occurred while deleting the user.");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _UserManagements.GetUsers();
            return Ok(users);
        }
    }
}
