using RYTUserManagementService.Common.Utilities;
using System.ComponentModel.DataAnnotations;

namespace RYTUserManagementService.Dto
{
    public class SchoolCreateDto
    {
        [Required]
        [StringLength(Constants.Max100Length)]
        public string SchoolName { get; set; }

        
        //public Address Address { get; set; }

        /*[Required]
        [StringLength(Constants.Max100Length)]
        public string SchoolType { get; set; }*/

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
