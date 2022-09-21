using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using RYTUserManagementService.Common.Utilities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RYTUserManagementService.Models
{
    public class Teacher : IdentityUser
    {
        public Constants.Titles Title { get; set; }

        [StringLength(Constants.Max100Length)]
        public string FullName { get; set; }

        [StringLength(Constants.Max200Length)]
        public string ProfileUrl { get; set; }

        [StringLength(Constants.Max100Length)]
        public string Position { get; set; }

        [StringLength(Constants.Max2000Length)]
        public string About { get; set; }

        public DateTime StartYear { get; set; }

        public DateTime? EndYear { get; set; }
        public string Address { get; set; }

        public virtual IEnumerable<School> School { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();

        public DateTime UpdateAt { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }





    }
}
