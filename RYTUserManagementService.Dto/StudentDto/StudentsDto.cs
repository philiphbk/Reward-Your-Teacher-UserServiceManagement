using RYTUserManagementService.Models;
using System.ComponentModel.DataAnnotations;

namespace RYTUserManagementService.Dto.StudentDto
{
    public class StudentsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string SchoolName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

    }
}
