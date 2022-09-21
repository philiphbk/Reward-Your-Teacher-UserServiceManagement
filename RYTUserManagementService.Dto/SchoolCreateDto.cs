using RYTUserManagementService.Common.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace RYTUserManagementService.Dto
{
    public class SchoolCreateDto
    {
        [Required]
        [StringLength(Constants.Max100Length)]
        public string SchoolName { get; set; }

        public Constants.SchoolType Type { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string Logo { get; set; }
        public string AddressId { get; set; }
        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}
