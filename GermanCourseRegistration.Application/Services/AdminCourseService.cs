using AutoMapper;
using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.Application.Messages.CourseMessages;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Services;

public class AdminCourseService : IAdminCourseService
{
    private readonly ICourseRepository courseRepository;
    private readonly IMapper mapper;

    public AdminCourseService(
        ICourseRepository courseRepository,
        IMapper mapper)
    {
        this.courseRepository = courseRepository;
        this.mapper = mapper;
    }

    public async Task<GetCourseByIdResponse> GetByIdAsync(GetCourseByIdRequest request)
    {
        var course = await courseRepository.GetByIdAsync(request.Id);

        var response = mapper.Map<GetCourseByIdResponse>(course);

        return response;
    }

    public async Task<GetAllCoursesResponse> GetAllAsync()
    {
        var courses = await courseRepository.GetAllAsync();

        var response = mapper.Map<GetAllCoursesResponse>(courses);

        return response;
    }

    public async Task<AddCourseResponse> AddAsync(AddCourseRequest request)
    {
        var course = mapper.Map<Course>(request);

        bool isAdded = await courseRepository.AddAsync(course);

        var response = new AddCourseResponse()
        {
            IsTransactionSuccess = isAdded,
            Message = isAdded
                ? "Course added successfully."
                : "Failed to add course."
        };

        return response;
    }

    public async Task<UpdateCourseResponse> UpdateAsync(UpdateCourseRequest request)
    {
        var course = mapper.Map<Course>(request);

        Course? updatedCourse = await courseRepository.UpdateAsync(course, request.Id);

        var response = mapper.Map<UpdateCourseResponse>((
            updatedCourse,
            updatedCourse != null,
            updatedCourse != null
            ? "Course updated successfully."
            : "Failed to update course."));

        return response;
    }

    public async Task<DeleteCourseResponse> DeleteAsync(DeleteCourseRequest request)
    {
        Course? deletedCourse =
            await courseRepository.DeleteAsync(request.Id);

        var response = mapper.Map<DeleteCourseResponse>((
            deletedCourse,
            deletedCourse != null,
            deletedCourse != null
            ? "Course deleted successfully."
            : "Failed to delete course."));

        return response;
    }
}
