using GermanCourseRegistration.Application.Messages.RegistrationMessages;
using GermanCourseRegistration.Application.ServiceResults;

namespace GermanCourseRegistration.Application.Services;

public interface IRegistrationService
{
    Task<AddRegistrationResponse> AddAsync(
        AddRegistrationRequest registrationRequest,
        AddOrderRequest orderRequest,
       AddOrderItemsRequest itemsRequest);

    Task<GetRegistrationByStudentIdResponse> GetByStudentIdAsync(
        GetRegistrationByStudentIdRequest request);
}
