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
                    Title = Constants.Titles.Dr,
                    FullName = "Ayooluwa Moses",
                    ProfileUrl = "https://unsplash.com/photos/mPnkjZ_9a8Q",
                    Position = "HeadSA",
                    About = "I am A Teacher",
                    School = new List<School>(),
                    Address = "Okuoromi Community,Benin, Edo, Nigeria, 9.0000000, 4.5646574",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "Dami",
                    UpdatedBy = "Dami"


                },
                new Teacher
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = Constants.Titles.Prof,
                    FullName = "Tijani Moses",
                    ProfileUrl = "https://unsplash.com/photos/mPnkjZ_9a8Q",
                    Position = "HeadSA",
                    About = "I am A Teacher",
                    School = new List<School>(),
                    Address = "Okuoromi Community,Benin, Edo, Nigeria, 9.0000000, 4.5646574",
                    CreatedAt = DateTime.Now,
                    CreatedBy = "Dami",
                    UpdatedBy = "Dami"
                }
            );
        }
    }
}
