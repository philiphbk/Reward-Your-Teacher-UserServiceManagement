using RYTUserManagementService.Common.Utilities;
using RYTUserManagementService.Models;

namespace RYTUserManagementService.Dto.TeacherDto;

public class UpdateTeacherDto
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? YearOfTeaching { get; set; }


    public string? SchoolName { get; set; }

    public string Subject { get; set; }


}