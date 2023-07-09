using AutoMapper;
using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.Application.Messages.PaymentMessages;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;

    public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
    }

    public async Task<AddPaymentResponse> AddAsync(AddPaymentRequest request)
    {
        var payment = mapper.Map<Payment>(request);

        bool isAdded = await paymentRepository.AddAsync(payment);

        var response = new AddPaymentResponse()
        {
            IsTransactionSuccess = isAdded,
            Message = isAdded
                ? "Payment added successfully."
                : "Failed to make a payment."
        };

        return response;
    }
}
