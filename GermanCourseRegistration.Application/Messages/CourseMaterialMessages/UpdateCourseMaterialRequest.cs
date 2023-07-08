namespace GermanCourseRegistration.Application.Messages.CourseMaterialMessages;

public record UpdateCourseMaterialRequest(
    Guid Id,
    string Name,
    string? Description,
    string Category,
    decimal Price,
    Guid LastModifiedBy,
    DateTime LastModifiedOn);