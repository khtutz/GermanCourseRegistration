﻿using AutoMapper;
using GermanCourseRegistration.Application.ServiceResults;
using GermanCourseRegistration.Web.Models.ViewModels;

namespace GermanCourseRegistration.Web.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CourseMaterialView, CourseMaterialResult>()
            .ForPath(d => d.CourseMaterial!.Id, opt => opt.MapFrom(s => s.Id))
            .ForPath(d => d.CourseMaterial!.Name, opt => opt.MapFrom(s => s.Name))
            .ForPath(d => d.CourseMaterial!.Description, opt => opt.MapFrom(s => s.Description))
            .ForPath(d => d.CourseMaterial!.Category, opt => opt.MapFrom(s => s.Category))
            .ForPath(d => d.CourseMaterial!.Price, opt => opt.MapFrom(s => s.Price))
            .ReverseMap();

        //CreateMap<CourseView, CourseResult>()
        //    .ForPath(d => d.Course!.Id, opt => opt.MapFrom(s => s.Id))
        //    .ForPath(d => d.Course!.Level, opt => opt.MapFrom(s => s.Level))
        //    .ForPath(d => d.Course!.Part, opt => opt.MapFrom(s => s.Part))
        //    .ForPath(d => d.Course!.Description, opt => opt.MapFrom(s => s.Description))
        //    .ReverseMap();
    }
}
