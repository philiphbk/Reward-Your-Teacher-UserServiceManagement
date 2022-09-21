using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using RYTUserManagementService.Common.Utilities;
using RYTUserManagementService.Models;

namespace RYTUserManagementService.Dto
{
    [AutoMapFrom(typeof(Address))]
    public class AddressDto
    {
        public int Id { get; set; }
        [StringLength(Constants.Max2000Length)]
        public string StreetAddress { get; set; }

        [StringLength(Constants.Max100Length)]
        public string City { get; set; }

        [StringLength(Constants.Max100Length)]
        public string State { get; set; }

        [StringLength(Constants.Max100Length)]
        public string Country { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
