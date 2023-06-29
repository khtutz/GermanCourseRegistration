using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.Application.ServiceResults;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Services;

public class RegistrationService : IRegistrationService
{
    private readonly IRegistrationRepository registrationRepository;

    public RegistrationService(IRegistrationRepository registrationRepository)
    {
        this.registrationRepository = registrationRepository;
    }

    public async Task<bool> AddAsync(dynamic model)
    {
        var registration = MapToRegistrationModel(model);

        bool isAdded = await registrationRepository.AddAsync(registration);

        return isAdded;
    }

    private Registration MapToRegistrationModel(dynamic model)
    {
        var registration = new Registration()
        {
            Id = model.Id,
            StudentId = model.StudentId,
            CourseOfferId = model.CourseOfferId,
            Status = model.Status,
            CreatedOn = model.CreatedOn,
            CourseMaterialOrder = model.CourseMaterialOrder
        };

        return registration;
    }

    public async Task<RegistrationResult> GetByStudentIdAsync(Guid id)
    {
        var registration = await registrationRepository.GetByStudentIdAsync(id);

        return new RegistrationResult(registration);
    }
}
