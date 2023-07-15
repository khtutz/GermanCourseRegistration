using GermanCourseRegistration.Application.Messages.CourseOfferMessages;
using GermanCourseRegistration.Web.Models.ViewModels;

namespace GermanCourseRegistration.Web.Mappings;

public static class CourseScheduleMapping
{
    public static IEnumerable<CourseScheduleView> MapToViewModels(
        GetAllCourseOffersResponse response)
    {
        var viewModels = new List<CourseScheduleView>();

        foreach (var schedule in response.CourseOffers)
        {
            viewModels.Add(new CourseScheduleView()
            {
                Id = schedule.Id,
                Name = schedule.Name,
                ClassType = schedule.ClassType,
                Cost = Convert.ToDecimal(schedule.Cost.ToString("0.####")),
                StartDate = schedule.StartDate,
                EndDate = schedule.EndDate,
                Course = schedule.Course != null
                ? new CourseView()
                {
                    Id = schedule.Course.Id,
                    Level = schedule.Course.Level,
                    Part = schedule.Course.Part,
                    Description = schedule.Course.Description
                } : null,
                Timetable = schedule.Timetables != null
                // Inefficient mapping!
                ? new TimetableView()
                {
                    StartTimeHour = schedule.Timetables.ToList()[0].StartTimeHour,
                    StartTimeMinute = schedule.Timetables.ToList()[0].StartTimeMinute,
                    EndTimeHour = schedule.Timetables.ToList()[0].EndTimeHour,
                    EndTimeMinute = schedule.Timetables.ToList()[0].EndTimeMinute
                } : new(),
                SelectDays = schedule.Timetables != null
                ? schedule.Timetables.Select(t => t.DayName)
                : Enumerable.Empty<string>()
            });
        }

        return viewModels;
    }

    public static CourseScheduleView MapToViewModel(GetCourseOfferByIdResponse response)
    {
        if (response == null)
        {
            throw new ArgumentNullException(
                nameof(response),
                "The response object cannot be null.");
        }

        if (response.CourseOffer == null)
        {
            throw new ArgumentNullException(
                nameof(response.CourseOffer),
                "The CourseMaterial property cannot be null.");
        }

        var schedule = response.CourseOffer;

        var viewModel = new CourseScheduleView()
        {
            Id = schedule.Id,
            Name = schedule.Name,
            ClassType = schedule.ClassType,
            Cost = Convert.ToDecimal(schedule.Cost.ToString("0.####")),
            StartDate = schedule.StartDate,
            EndDate = schedule.EndDate,
            Course = schedule.Course != null
                ? new CourseView()
                {
                    Id = schedule.Course.Id,
                    Level = schedule.Course.Level,
                    Part = schedule.Course.Part,
                    Description = schedule.Course.Description
                } : null,
            Timetable = schedule.Timetables != null
                // Inefficient mapping!
                ? new TimetableView()
                {
                    StartTimeHour = schedule.Timetables.ToList()[0].StartTimeHour,
                    StartTimeMinute = schedule.Timetables.ToList()[0].StartTimeMinute,
                    EndTimeHour = schedule.Timetables.ToList()[0].EndTimeHour,
                    EndTimeMinute = schedule.Timetables.ToList()[0].EndTimeMinute
                } : new(),
            SelectDays = schedule.Timetables != null
                ? schedule.Timetables.Select(t => t.DayName)
                : Enumerable.Empty<string>()
        };

        return viewModel;
    }

    public static AddCourseOfferRequest MapToAddRequest(
        CourseScheduleView viewModel, Guid createdBy, DateTime createdOn)
    {
        var request = new AddCourseOfferRequest(
            viewModel.Course!.Id,
            viewModel.Name,
            viewModel.ClassType,
            viewModel.Cost,
            viewModel.StartDate,
            viewModel.EndDate,
            createdBy,
            createdOn,
            viewModel.DaysOfWeek,
            viewModel.Timetable.StartTimeHour,
            viewModel.Timetable.StartTimeMinute,
            viewModel.Timetable.EndTimeHour,
            viewModel.Timetable.EndTimeMinute);

        return request;
    }

    public static UpdateCourseOfferRequest MapToUpdateRequest(
        CourseScheduleView viewModel, Guid lastModifiedBy, DateTime lastModifiedOn)
    {
        var request = new UpdateCourseOfferRequest(
            viewModel.Id,
            viewModel.Course!.Id,
            viewModel.Name,
            viewModel.ClassType,
            viewModel.Cost,
            viewModel.StartDate,
            viewModel.EndDate,
            lastModifiedBy,
            lastModifiedOn,
            viewModel.DaysOfWeek,
            viewModel.Timetable.StartTimeHour,
            viewModel.Timetable.StartTimeMinute,
            viewModel.Timetable.EndTimeHour,
            viewModel.Timetable.EndTimeMinute);

        return request;
    }
}
