using AutoMapper;
using RYTUserManagementService.Models;

namespace RYTUserManagementService.Dto
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Student, StudentsDto>().ReverseMap();
            CreateMap<Student, CreateStudentDto>().ReverseMap();
            CreateMap<Student, UpdateStudentDto>().ReverseMap();
            CreateMap<Teacher, TeacherDto>().ReverseMap();
            CreateMap<Teacher, UpdateTeacherDto>().ReverseMap();
            CreateMap<School, SchoolViewDto>().ReverseMap();
            CreateMap<School, SchoolCreateDto>().ReverseMap();
            CreateMap<School, UpdateStudentDto>().ReverseMap();

            CreateMap<ApiUser, UserDto>().ReverseMap();

        }
    }
}
