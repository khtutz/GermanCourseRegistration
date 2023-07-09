namespace GermanCourseRegistration.Application.Messages.StudentMessages;

public record AddStudentRequest(
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
    DateTime CreatedOn);