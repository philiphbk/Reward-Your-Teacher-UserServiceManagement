using System.ComponentModel.DataAnnotations;

namespace RYTUserManagementService.Dto.UserDto;

public class LoginUserDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [StringLength(15, ErrorMessage = "Your Password is limited to {2} to 15 characters", MinimumLength = 4)]
    public string Password { get; set; }

}