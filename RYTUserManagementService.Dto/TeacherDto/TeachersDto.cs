using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYTUserManagementService.Dto.TeacherDto
{
    public class TeachersDto
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? SchoolName { get; set; }
        public string? About { get; set; }


        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
    }
}
