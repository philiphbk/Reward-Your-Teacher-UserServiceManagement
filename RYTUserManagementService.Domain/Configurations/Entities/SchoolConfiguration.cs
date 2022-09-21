using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RYTUserManagementService.Common.Utilities;
using RYTUserManagementService.Models;

namespace RYTUserManagementService.Domain.Configurations.Entities
{
    public class SchoolConfiguration : IEntityTypeConfiguration<School>
    {
        private readonly Address _address = new Address();
        public void Configure(EntityTypeBuilder<School> builder)
        {

            builder.HasData(
                new School
                {

                    Id = "11f09734-289d-11ed-a261-0242ac120002",
                    SchoolName = "Decagon Institute Edo",
                    AddressId = "a7dd2ab0-289c-11ed-a261-0242ac120002",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Today,

                },
                new School
                {

                    Id = "21addd9e-289d-11ed-a261-0242ac120002",
                    SchoolName = "Decagon Institute Lagos",
                    AddressId = "b01430ca-289c-11ed-a261-0242ac120002",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Today,
                }
            );

        }
    }
}
