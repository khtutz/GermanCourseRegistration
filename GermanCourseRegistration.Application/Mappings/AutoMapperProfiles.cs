using AutoMapper;
using GermanCourseRegistration.Application.Messages.CourseMaterialMessages;
using GermanCourseRegistration.Application.Messages.CourseMessages;
using GermanCourseRegistration.Application.Messages.CourseOfferMessages;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // CourseMaterial
        CreateMap<CourseMaterial, GetCourseMaterialByIdResponse>()
            .ForMember(d => d.CourseMaterial, opt => opt.MapFrom(s => s));
        CreateMap<IEnumerable<CourseMaterial>, GetAllCourseMaterialsResponse>()
            .ForMember(d => d.CourseMaterials, opt => opt.MapFrom(s => s));

        CreateMap<AddCourseMaterialRequest, CourseMaterial>();

        CreateMap<UpdateCourseMaterialRequest, CourseMaterial>();
        CreateMap<(CourseMaterial, bool, string), UpdateCourseMaterialResponse>()
            .ForMember(d => d.CourseMaterial, opt => opt.MapFrom(s => s.Item1))
            .ForMember(d => d.IsTransactionSuccess, opt => opt.MapFrom(s => s.Item2))
            .ForMember(d => d.Message, opt => opt.MapFrom(s => s.Item3));

        CreateMap<(CourseMaterial, bool, string), DeleteCourseMaterialResponse>()
            .ForMember(d => d.CourseMaterial, opt => opt.MapFrom(s => s.Item1))
            .ForMember(d => d.IsTransactionSuccess, opt => opt.MapFrom(s => s.Item2))
            .ForMember(d => d.Message, opt => opt.MapFrom(s => s.Item3));

        // Course
        CreateMap<Course, GetCourseByIdResponse>()
            .ForMember(d => d.Course, opt => opt.MapFrom(s => s));
        CreateMap<IEnumerable<Course>, GetAllCoursesResponse>()
            .ForMember(d => d.Courses, opt => opt.MapFrom(s => s));

        CreateMap<AddCourseRequest, Course>();

        CreateMap<UpdateCourseRequest, Course>();
        CreateMap<(Course, bool, string), UpdateCourseResponse>()
            .ForMember(d => d.Course, opt => opt.MapFrom(s => s.Item1))
            .ForMember(d => d.IsTransactionSuccess, opt => opt.MapFrom(s => s.Item2))
            .ForMember(d => d.Message, opt => opt.MapFrom(s => s.Item3));

        CreateMap<(Course, bool, string), DeleteCourseResponse>()
            .ForMember(d => d.Course, opt => opt.MapFrom(s => s.Item1))
            .ForMember(d => d.IsTransactionSuccess, opt => opt.MapFrom(s => s.Item2))
            .ForMember(d => d.Message, opt => opt.MapFrom(s => s.Item3));

        // CourseOffer
        CreateMap<CourseOffer, GetCourseOfferByIdResponse>()
            .ForMember(d => d.CourseOffer, opt => opt.MapFrom(s => s));
        CreateMap<IEnumerable<CourseOffer>, GetAllCourseOffersResponse>()
            .ForMember(d => d.CourseOffers, opt => opt.MapFrom(s => s));

        CreateMap<AddCourseOfferRequest, CourseOffer>();

        CreateMap<UpdateCourseOfferRequest, CourseOffer>();
        CreateMap<(CourseOffer, bool, string), UpdateCourseOfferResponse>()
            .ForMember(d => d.CourseOffer, opt => opt.MapFrom(s => s.Item1))
            .ForMember(d => d.IsTransactionSuccess, opt => opt.MapFrom(s => s.Item2))
            .ForMember(d => d.Message, opt => opt.MapFrom(s => s.Item3));

        CreateMap<(CourseOffer, bool, string), DeleteCourseOfferResponse>()
            .ForMember(d => d.CourseOffer, opt => opt.MapFrom(s => s.Item1))
            .ForMember(d => d.IsTransactionSuccess, opt => opt.MapFrom(s => s.Item2))
            .ForMember(d => d.Message, opt => opt.MapFrom(s => s.Item3));
    }
}
