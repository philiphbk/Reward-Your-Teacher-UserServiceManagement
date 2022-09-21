using RYTUserManagementService.Domain.RepoImplementations;
using RYTUserManagementService.Domain.RepoInterfaces;
using RYTUserManagementService.Dto;
using RYTUserManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYTUserManagementService.Domain.TypeRepository
{
    public class StudentRepository : GenericRepository<CreateStudentDto>, IStudentRepository
    {
        public StudentRepository(UserManagementDbContext context) : base(context) { }
    }


}
