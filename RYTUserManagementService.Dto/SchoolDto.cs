using RYTUserManagementService.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using RYTUserManagementService.Common.Utilities;

namespace RYTUserManagementService.Dto
{
    public class SchoolDto 
    {

        public string Id { get; set; }

        public string SchoolName { get; set; }
        public string AddressId { get; set; }
        public string CreatedBy { get; set; }
    }

}
