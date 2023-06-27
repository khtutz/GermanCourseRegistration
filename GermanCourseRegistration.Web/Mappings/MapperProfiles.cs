using GermanCourseRegistration.Application.ServiceResults;
using GermanCourseRegistration.Web.Models.ViewModels;

namespace GermanCourseRegistration.Web.Mappings;

public static class MapperProfiles
{
    public static List<UserIndividualView> MapUserResultsToUserIndividualViews(
        IEnumerable<UserResult> users)
    {
        var userIndividualViews = new List<UserIndividualView>();

        foreach (var user in users)
        {
            if (user.User != null)
            {
                userIndividualViews.Add(new UserIndividualView
                {
                    Id = Guid.Parse(user.User.Id),
                    Username = user.User.UserName!,
                    EmailAddress = user.User.Email!
                });
            }
        }

        return userIndividualViews;
    }

    public static CourseScheduleView MapCourseOfferResultToCourseScheduleViewModel(
        CourseOfferResult courseOfferResult)
    {
        return new CourseScheduleView
        {
            Id = courseOfferResult.CouseOffer!.Id,
            Name = courseOfferResult.CouseOffer!.Name,
            ClassType = courseOfferResult.CouseOffer!.ClassType,
            Cost = Convert.ToDecimal(courseOfferResult.CouseOffer!.Cost.ToString("0.####")),
            StartDate = courseOfferResult.CouseOffer!.StartDate,
            EndDate = courseOfferResult.CouseOffer!.EndDate,
            Course = courseOfferResult.CouseOffer!.Course != null
                ? new CourseView()
                {
                    Id = courseOfferResult.CouseOffer!.Course.Id,
                    Level = courseOfferResult.CouseOffer!.Course.Level,
                    Part = courseOfferResult.CouseOffer!.Course.Part,
                    Description = courseOfferResult.CouseOffer!.Course.Description
                } : null,
            Timetable = courseOfferResult.CouseOffer!.Timetables != null
                // Inefficient mapping!
                ? new TimetableView()
                {
                    StartTimeHour = courseOfferResult.CouseOffer!.Timetables.ToList()[0].StartTimeHour,
                    StartTimeMinute = courseOfferResult.CouseOffer!.Timetables.ToList()[0].StartTimeMinute,
                    EndTimeHour = courseOfferResult.CouseOffer!.Timetables.ToList()[0].EndTimeHour,
                    EndTimeMinute = courseOfferResult.CouseOffer!.Timetables.ToList()[0].EndTimeMinute
                } : new(),
            SelectDays = courseOfferResult.CouseOffer!.Timetables != null
                ? courseOfferResult.CouseOffer!.Timetables.Select(t => t.DayName)
                : Enumerable.Empty<string>()
        };
    }
}
