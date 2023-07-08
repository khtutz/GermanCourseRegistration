namespace GermanCourseRegistration.Application.Messages.CourseMaterialMessages;

public record AddCourseMaterialRequest(
    string Name,
    string? Description,
    string Category,
    decimal Price,
    Guid CreatedBy,
    DateTime CreatedOn);
