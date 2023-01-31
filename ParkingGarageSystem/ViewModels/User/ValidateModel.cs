using System.ComponentModel.DataAnnotations;

namespace ParkingGarageSystem.ViewModels.User
{
    public class ValidateModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
