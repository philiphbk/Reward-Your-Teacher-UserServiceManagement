using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using RYTUserManagementService.Common.Utilities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RYTUserManagementService.Models
{
    public class Teacher : ApiUser
    {
        [StringLength(Constants.Max100Length)]
        public string FullName { get; set; }

        [StringLength(Constants.Max2000Length)]
        public string About { get; set; }

        public DateTime StartYear { get; set; }

        public DateTime EndYear { get; set; }
        public string Address { get; set; }
        public string SchoolId { get; set; }

        [ForeignKey("SchoolId")]
        public School School { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();

    }
}
