namespace GermanCourseRegistration.Application.Messages.CourseMessages;

public record UpdateCourseRequest(
    Guid Id,
    string Level,
    int Part,
    string? Description,
    Guid LastModifiedBy,
    DateTime LastModifiedOn);