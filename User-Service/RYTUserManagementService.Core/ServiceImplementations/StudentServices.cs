using RYTUserManagementService.Core.ServiceInterfaces;
using RYTUserManagementService.Domain.RepoInterfaces;
using RYTUserManagementService.Dto;
using RYTUserManagementService.Models;

namespace RYTUserManagementService.Core.ServiceImplementations
{
    public class StudentServices : IStudentServices
    {/*
        private readonly IStudentRepository _studentRepo;
        public StudentServices(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }
        public async Task<IEnumerable<CreateStudentDTO>> GetAllStudentsAsync()
        {
            return _studentRepo.GetAll();
        }
        public async Task<CreateStudentDTO> GetStudentByIdAsync(int id)
        {
            return _studentRepo.GetById(id);
        }
        public async Task<CreateStudentDTO> GetStudentBySchoolIdAsync(int schoolId)
        {
            return _studentRepo.GetById(schoolId);
        }
        public async Task<CreateStudentDTO> AddStudent(CreateStudentDTO student)
        {
            return _studentRepo.Add(student);
        }
        public async Task<CreateStudentDTO> UpdateStudent(int id)
        {
            return _studentRepo.UpdateStudent(id);
        }
        public async Task<CreateStudentDTO> DeleteStudent(CreateStudentDTO student)
        {
            return _studentRepo.Remove(student);
        }*/
    }
}
