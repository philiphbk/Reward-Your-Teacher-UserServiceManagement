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
    public class StudentConfiguration: IEntityTypeConfiguration<Student>
    {
      
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData(
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "bayo",
                    LastName = "dayo",
                    SchoolName = "Lara&Manny Int'l sec school",
                    FullName = "Jegede Moses",
                    About = "I am a student",
                    Address = "Okuoromi Community,Benin, Edo, Nigeria, 9.0000000, 4.5646574",
                    CreatedAt = DateTime.Now,
                  
                },
                new Student
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "bayo",
                    LastName = "dayo",
                   
                    SchoolName = "Lara&Manny Int'l sec school",
                    FullName = "Jegede Esther",
                   
                    About = "I am a student",
                    Address = "Okuoromi Community,Benin, Edo, Nigeria, 9.0000000, 4.5646574",
                    CreatedAt = DateTime.Now,
                }
            ); ;
        }
    }
    
}