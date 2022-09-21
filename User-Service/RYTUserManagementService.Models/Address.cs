using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RYTUserManagementService.Common.Utilities;

namespace RYTUserManagementService.Models
{
    [Table(name: "Addresses")]
    public class Address : BaseEntity
    {
        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public ICollection<School> Schools { get; set; }
    }
}
