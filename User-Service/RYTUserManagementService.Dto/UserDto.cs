using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using RYTUserManagementService.Models;
using Abp.AutoMapper;

namespace RYTUserManagementService.Dto
{
    [AutoMapFrom(typeof(ApiUser))]
    public class LoginUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Your Password is limited to {2} to 15 characters", MinimumLength = 4)]
        public string Password { get; set; }

        public ICollection<string> Roles { get; set; }
    }

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

    public class TokenDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }

    public class UpdatePasswordDto : UserDto
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
}
}
