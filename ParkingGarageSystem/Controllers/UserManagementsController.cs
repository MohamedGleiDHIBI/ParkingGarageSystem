using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;
using ParkingGarageSystem.ViewModels.User;

namespace ParkingGarageSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementsController : ControllerBase
    {
        private readonly IUserManagements _UserManagements;
        private readonly IMapper _Mapper;
        public UserManagementsController(IUserManagements userManagements, IMapper Mapper)
        {
            _UserManagements = userManagements;
            _Mapper = Mapper;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var user = _Mapper.Map<User>(registerView);
            var existingUser = await _UserManagements.GetUserByEmail(registerView.Email);
            if (existingUser != null)
            {
                return BadRequest("User already exists");
            }
            user.Password = new PasswordHasher<object?>().HashPassword(user, registerView.Password);
            var result = await _UserManagements.CreateUser(user);
            if (!result)
            {
                return BadRequest(new { message = "An error occurred while creating the user" });
            }
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _UserManagements.GetUserByEmail(loginModel.Email);
            if (user == null)
            {
                return NotFound("Sorry, we couldn't find a user with that username. Please double-check the username and try again, or create a new account");
            }
            if (new PasswordHasher<object?>().VerifyHashedPassword(null, user.Password, loginModel.Password) ==PasswordVerificationResult.Failed)
            {
                return BadRequest("The password you entered is incorrect. Please try again or reset your password.");
            }

            return Ok();
        }
        
        [HttpPost]
        [Route("validate")]
        public async Task<IActionResult> Validate([FromBody] ValidateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _UserManagements.Validate(model.Email, model.Token);
            if (!result)
            {
                return BadRequest(new { message = "Invalid token or email" });
            }
            return Ok();
        }
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
        [HttpPut]
        [Route("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var userId = int.Parse(User.Identity.Name);
            var user = await _UserManagements.GetUserById(userId);
            if (user == null)
            {
                return NotFound("Sorry, we couldn't find a user with that username. Please double-check the username and try again, or create a new account");
            }

            if (new PasswordHasher<object?>().VerifyHashedPassword(null, user.Password, model.CurrentPassword) == PasswordVerificationResult.Failed)
            {
                return BadRequest("The current password you entered is incorrect. Please double-check and try again.");
            }
            user.Password = new PasswordHasher<object?>().HashPassword(user, model.NewPassword);
            var result = await _UserManagements.UpdateUser(user);
            if (result)
            {
                return Ok();
            }
            return BadRequest("An error occurred while changing the password.");
        }
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
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _UserManagements.GetUsers();
            return Ok(users);
        }
    }
}
