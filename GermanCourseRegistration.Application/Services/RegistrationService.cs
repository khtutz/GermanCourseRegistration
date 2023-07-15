using AutoMapper;
using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.Application.Messages.RegistrationMessages;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Services;

public class RegistrationService : IRegistrationService
{
    private readonly IRegistrationRepository registrationRepository;
    private readonly IMapper mapper;

    public RegistrationService(
        IRegistrationRepository registrationRepository,
        IMapper mapper)
    {
        this.registrationRepository = registrationRepository;
        this.mapper = mapper;
    }

    public async Task<AddRegistrationResponse> AddAsync(
        AddRegistrationRequest registrationRequest,
        AddOrderRequest orderRequest,
        AddOrderItemsRequest itemsRequest)
    {
        var registration = MapToRegistrationModel(
            registrationRequest, orderRequest, itemsRequest);

        bool isAdded = await registrationRepository.AddAsync(registration);

        var response = new AddRegistrationResponse()
        {
            IsTransactionSuccess = isAdded,
            Message = isAdded
                ? "Course and materials added successfully."
                : "Failed to add course and materials."
        };
        return response;
    }

    public async Task<GetRegistrationByStudentIdResponse> GetByStudentIdAsync(
        GetRegistrationByStudentIdRequest request)
    {
        var registration = await registrationRepository.GetByStudentIdAsync(request.Id);

        var response = mapper.Map<GetRegistrationByStudentIdResponse>(registration);

        return response;
    }

    private Registration MapToRegistrationModel(
        AddRegistrationRequest registrationRequest,
        AddOrderRequest orderRequest,
        AddOrderItemsRequest itemsRequest)
    {
        var order = mapper.Map<CourseMaterialOrder>((orderRequest, itemsRequest));
        var registration = mapper.Map<Registration>((registrationRequest, order));

        return registration;
    }
}
