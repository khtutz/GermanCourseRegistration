namespace GermanCourseRegistration.Application.Messages.CourseOfferMessages;

public record UpdateCourseOfferRequest(
    Guid Id,
    Guid CourseId,
    string Name,
    string ClassType,
    decimal Cost,
    DateTime StartDate,
    DateTime EndDate,
    Guid LastModifiedBy,
    DateTime LastModifiedOn,
    IEnumerable<string> DaysOfWeek,
    int StartTimeHour,
    int StartTimeMinute,
    int EndTimeHour,
    int EndTimeMinute);
