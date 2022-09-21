using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYTUserManagementService.Models
{
    public class BaseEntity
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();

        public DateTime UpdatedAt { get; set; }

        public string CreatedBy { get; set; }
        
        public string UpdatedBy { get; set; }
    }
}
