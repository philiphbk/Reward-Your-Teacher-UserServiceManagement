using System.ComponentModel.DataAnnotations;

namespace RYTUserManagementService.Dto.UserDto;

public class UpdatePasswordDto
{
    [Required]
    [Display(Name = "Email Address")]
    public string Email { get; set; }
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