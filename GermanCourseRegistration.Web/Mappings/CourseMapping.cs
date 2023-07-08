using GermanCourseRegistration.Application.Messages.CourseMessages;
using GermanCourseRegistration.Web.Models.ViewModels;

namespace GermanCourseRegistration.Web.Mappings;

public static class CourseMapping
{
    public static IEnumerable<CourseView> MapToViewModels(
        GetAllCoursesResponse response)
    {
        var viewModels = new List<CourseView>();

        foreach (var Course in response.Courses)
        {
            viewModels.Add(new CourseView()
            {
                Id = Course.Id,
                Level = Course.Level,
                Part = Course.Part,
                Description = Course.Description
            });
        }

        return viewModels;
    }

    public static CourseView MapToViewModel(
        GetCourseByIdResponse response)
    {
        if (response == null)
        {
            throw new ArgumentNullException(
                nameof(response),
                "The response object cannot be null.");
        }

        if (response.Course == null)
        {
            throw new ArgumentNullException(
                nameof(response.Course),
                "The Course property cannot be null.");
        }

        var viewModel = new CourseView()
        {
            Id = response.Course.Id,
            Level = response.Course.Level,
            Part = response.Course.Part,
            Description = response.Course.Description,
        };

        return viewModel;
    }

    public static AddCourseRequest MapToAddRequest(
        CourseView viewModel, Guid createdBy, DateTime createdDT)
    {
        var request = new AddCourseRequest(
            viewModel.Level,
            viewModel.Part,
            viewModel.Description,
            createdBy,
            createdDT);

        return request;
    }

    public static UpdateCourseRequest MapToUpdateRequest(
        CourseView viewModel, Guid lastModifiedBy, DateTime lastModifiedOn)
    {
        var request = new UpdateCourseRequest(
            viewModel.Id,
            viewModel.Level,
            viewModel.Part,
            viewModel.Description,
            lastModifiedBy,
            lastModifiedOn);

        return request;
    }
}
