using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Messages.RegistrationMessages;

public record GetRegistrationByStudentIdResponse : BaseResponse
{
    public Registration? Registration { get; init; }
}