using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RYTUserManagementService.Common.Utilities;
using RYTUserManagementService.Models;

namespace RYTUserManagementService.Domain.Configurations.Entities
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {

            builder.HasData(
                new Address
                {
                    Id = "a7dd2ab0-289c-11ed-a261-0242ac120002",
                    StreetAddress = "Okuoromi Community",
                    City = "Benin",
                    State = "Edo",
                    Country = "Nigeria",
                    Longitude = 9.0000000,
                    Latitude = 4.5646574,
                },
                new Address
                {

                    Id = "b01430ca-289c-11ed-a261-0242ac120002",
                    StreetAddress = "Lagos Community",
                    City = "Lagos",
                    State = "Lagos",
                    Country = "Nigeria",
                    Longitude = 9.560064570,
                    Latitude = 4.56467646574,
                }

            );
        }
    }

}
