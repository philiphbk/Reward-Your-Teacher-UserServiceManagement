using AutoMapper;
using RYTUserManagementService.Dto.StudentDto;
using RYTUserManagementService.Dto.TeacherDto;
using RYTUserManagementService.Models;

namespace RYTUserManagementService.Dto
{
    public class Mappings : Profile
    {
        public Mappings()
        {

            CreateMap<Student, StudentsDto>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ReverseMap();
            CreateMap<Student, UpdateStudentDto>().ReverseMap();
            CreateMap<Teacher, CreateTeacherDto>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ReverseMap(); ;
            CreateMap<Teacher, UpdateTeacherDto>().ReverseMap();
            CreateMap<School, SchoolViewDto>().ReverseMap();
            CreateMap<School, SchoolCreateDto>().ReverseMap();
            CreateMap<School, UpdateStudentDto>().ReverseMap();

            CreateMap<ApiUser, UserDto.UserDto>().ReverseMap();
            CreateMap<Teacher, TeachersDto>().ReverseMap();

        }
    }
}
