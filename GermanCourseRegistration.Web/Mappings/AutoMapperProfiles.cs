using AutoMapper;
using GermanCourseRegistration.Application.ServiceResults;
using GermanCourseRegistration.Web.Models.ViewModels;

namespace GermanCourseRegistration.Web.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CourseMaterialResult, CourseMaterialView>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CourseMaterial!.Id))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.CourseMaterial!.Name))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.CourseMaterial!.Description))
            .ForMember(d => d.Category, opt => opt.MapFrom(s => s.CourseMaterial!.Category))
            .ForMember(d => d.Price, opt => opt.MapFrom(s => s.CourseMaterial!.Price));

        CreateMap<CourseResult, CourseView>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Course!.Id))
            .ForMember(d => d.Level, opt => opt.MapFrom(s => s.Course!.Level))
            .ForMember(d => d.Part, opt => opt.MapFrom(s => s.Course!.Part))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Course!.Description));

        CreateMap<StudentResult, StudentView>()
            .ForPath(d => d.Id, opt => opt.MapFrom(s => s.Student!.Id))
            .ForPath(d => d.Salutation, opt => opt.MapFrom(s => s.Student!.Salutation))
            .ForPath(d => d.FirstName, opt => opt.MapFrom(s => s.Student!.FirstName))
            .ForPath(d => d.LastName, opt => opt.MapFrom(s => s.Student!.LastName))
            .ForPath(d => d.Birthday, opt => opt.MapFrom(s => s.Student!.Birthday))
            .ForPath(d => d.Gender, opt => opt.MapFrom(s => s.Student!.Gender))
            .ForPath(d => d.Mobile, opt => opt.MapFrom(s => s.Student!.Mobile))
            .ForPath(d => d.Email, opt => opt.MapFrom(s => s.Student!.Email))
            .ForPath(d => d.Address, opt => opt.MapFrom(s => s.Student!.Address))
            .ForPath(d => d.PostalCode, opt => opt.MapFrom(s => s.Student!.PostalCode));
    }
}
