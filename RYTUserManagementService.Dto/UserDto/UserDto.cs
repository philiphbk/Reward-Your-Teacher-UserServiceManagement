using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using RYTUserManagementService.Models;

namespace RYTUserManagementService.Dto.UserDto
{
    public class UserDto : LoginUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Password)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Your Password is limited to {2} to 15 characters", MinimumLength = 4)]
        public string Password { get; set; }

    }
}
