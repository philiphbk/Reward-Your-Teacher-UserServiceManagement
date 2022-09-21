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
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
    
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasData(
                new Teacher
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "bayo",
                    LastName = "dayo",
               
                    FullName = "Ayooluwa Moses",
                  
                    About = "I am A Teacher",
                    SchoolId = "11f09734-289d-11ed-a261-0242ac120002",
                    
                    Address = "Okuoromi Community,Benin, Edo, Nigeria, 9.0000000, 4.5646574",
                    CreatedAt = DateTime.Now,
                  


                },
                new Teacher
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "bayo",
                    LastName = "dayo",
                 
                    FullName = "Tijani Moses",
                   
                    About = "I am A Teacher",
                    SchoolId = "11f09734-289d-11ed-a261-0242ac120002",
                    Address = "Okuoromi Community,Benin, Edo, Nigeria, 9.0000000, 4.5646574",
                    CreatedAt = DateTime.Now,
                   
                }
            );
        }
    }
}
