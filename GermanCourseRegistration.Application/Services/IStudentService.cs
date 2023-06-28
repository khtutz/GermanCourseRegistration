using GermanCourseRegistration.Application.ServiceResults;

namespace GermanCourseRegistration.Application.Services;

public interface IStudentService
{
    Task<StudentResult> GetByIdAsync(Guid id);

    Task<bool> AddAsync(
        string salutation,
        string firstName,
        string lastName,
        DateTime birthday,
        string gender,
        string mobile,
        string email,
        string address,
        string postalCode,
        DateTime createdOn);

    Task<StudentResult> UpdateAsync(
        Guid id,
        string salutation,
        string firstName,
        string lastName,
        DateTime birthday,
        string gender,
        string mobile,
        string email,
        string address,
        string postalCode,
        DateTime lastModifiedOn);

    IEnumerable<string> GetSalutations();
}
