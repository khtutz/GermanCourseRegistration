namespace GermanCourseRegistration.Application.Messages.CourseOfferMessages;

public record AddCourseOfferRequest(
    Guid CourseId,
    string Name,
    string ClassType,
    decimal Cost,
    DateTime StartDate,
    DateTime EndDate,
    Guid CreatedBy,
    DateTime CreatedOn,
    IEnumerable<string> DaysOfWeek,
    int StartTimeHour,
    int StartTimeMinute,
    int EndTimeHour,
    int EndTimeMinute);
