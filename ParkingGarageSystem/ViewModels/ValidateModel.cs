using System.ComponentModel.DataAnnotations;

namespace ParkingGarageSystem.ViewModels
{
    public class ValidateModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
