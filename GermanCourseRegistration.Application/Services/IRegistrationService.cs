using GermanCourseRegistration.Application.ServiceResults;

namespace GermanCourseRegistration.Application.Services;

public interface IRegistrationService
{
    Task<bool> AddAsync(
        dynamic registrationModel, 
        dynamic orderModel, 
        List<dynamic> itemModels);

    Task<RegistrationResult> GetByStudentIdAsync(Guid id);
}
