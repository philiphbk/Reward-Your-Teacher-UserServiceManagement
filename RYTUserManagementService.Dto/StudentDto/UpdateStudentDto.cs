namespace RYTUserManagementService.Dto.StudentDto;

public class UpdateStudentDto : StudentsDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public DateTime UpdateAt { get; set; } = DateTime.Now;


}