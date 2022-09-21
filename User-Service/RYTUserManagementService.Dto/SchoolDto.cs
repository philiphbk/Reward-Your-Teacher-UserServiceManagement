using RYTUserManagementService.Models;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Microsoft.AspNetCore.Identity;
using RYTUserManagementService.Common.Utilities;

namespace RYTUserManagementService.Dto
{
    [AutoMapFrom(typeof(School))]
    public class SchoolDto 
    {

        public string Id { get; set; }

        public string SchoolName { get; set; }

       // public string Address { get; set; }

       // public DateTime CreatedAt { get; set; }
    }


    /*
    [AutoMapFrom(typeof(School))]
    public class UpdateSchoolDto : SchoolDto
    {
        [StringLength(Constants.Max100Length)]
        public School Logo { get; set; }

        public School SchoolName { get; set; }

        public IdentityUser Password { get; set; }

        public IdentityUser PhoneNumber { get; set; }

        public Address Address { get; set; }

        public Constants.SchoolType SchoolType { get; set; }

        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

    }*/
}
