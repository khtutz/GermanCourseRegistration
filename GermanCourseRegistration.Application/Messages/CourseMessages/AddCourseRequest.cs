namespace GermanCourseRegistration.Application.Messages.CourseMessages;

public record AddCourseRequest(
    string Level,
    int Part,
    string? Description,
    Guid CreatedBy,
    DateTime CreatedOn);
