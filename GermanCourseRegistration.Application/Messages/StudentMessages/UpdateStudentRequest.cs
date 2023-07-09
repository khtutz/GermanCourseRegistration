namespace GermanCourseRegistration.Application.Messages.StudentMessages;

public record UpdateStudentRequest(
    Guid Id,
    string Salutation,
    string FirstName,
    string LastName,
    DateTime Birthday,
    string Gender,
    string Mobile,
    string Email,
    string Address,
    string PostalCode,
    DateTime LastModifiedOn);