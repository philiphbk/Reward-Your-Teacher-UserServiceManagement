using Abp.AutoMapper;
using Microsoft.AspNetCore.Identity;
using RYTUserManagementService.Common.Utilities;
using RYTUserManagementService.Models;

namespace RYTUserManagementService.Dto
{
    [AutoMapFrom(typeof(Teacher))]
    public class TeacherDto
    {
        public int Id { get; set; }
        public Constants.Titles Title { get; set; }
    
        public string ProfileUrl { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public IdentityUser Password { get; set; }

        public IdentityUser PhoneNumber { get; set; }

        public IdentityUser Position { get; set; }

        public Address Address { get; set; }

        public SchoolDto SchoolName { get; set; }

        public IdentityUser UserType { get; set; }

        public IdentityUser Gender { get; set; }

        public TeacherDto About { get; set; }

        public TeacherDto StartYear { get; set; }

        public TeacherDto? EndYear { get; set; }
    }

    [AutoMapFrom(typeof(Teacher))]
    public class UpdateTeacherDto : TeacherDto
    {
        public Constants.Titles Title { get; set; }

        public string ProfileUrl { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public IdentityUser Password { get; set; }

        public IdentityUser PhoneNumber { get; set; }

        public IdentityUser Position { get; set; }

        public Address Address { get; set; }

        public SchoolDto SchoolName { get; set; }

        public IdentityUser UserType { get; set; }

        public IdentityUser Gender { get; set; }

        public TeacherDto About { get; set; }

        public TeacherDto StartYear { get; set; }

        public Teacher? EndYear { get; set; }
    }
}
