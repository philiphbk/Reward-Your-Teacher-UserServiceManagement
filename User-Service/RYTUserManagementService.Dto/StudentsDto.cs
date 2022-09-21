using Abp.AutoMapper;
using Microsoft.AspNetCore.Identity;
using RYTUserManagementService.Models;

namespace RYTUserManagementService.Dto
{
    [AutoMapFrom(typeof(Student))]
    public class StudentsDto 
    {
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SchoolName { get; set; }

    }


    [AutoMapFrom(typeof(Student))]
    public class CreateStudentDto : StudentsDto
    {
        public int Id { get; set; }
        public Student Title { get; set; }
        public string FullName { get; set; }
        public string ProfileUrl { get; set; }

        public string Email { get; set; }

        public IdentityUser Password { get; set; }

        public IdentityUser PhoneNumber { get; set; }

        public Address Address { get; set; }

        public School SchoolName { get; set; }

        public IdentityUser UserType { get; set; }

        public IdentityUser Gender { get; set; }

        public Student About { get; set; }

    }

    [AutoMapFrom(typeof(Student))]
    public class UpdateStudentDto : StudentsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public DateTime UpdateAt { get; set; } = DateTime.Now;

        public int SchoolId { get; set; }

        public ICollection<StudentsDto> Students { get; set; }
    }
}
