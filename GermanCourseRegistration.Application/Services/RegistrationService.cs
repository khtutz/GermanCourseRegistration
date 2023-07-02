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

    public async Task<bool> AddAsync(
        dynamic registrationModel,
        dynamic orderModel,
        List<dynamic> itemModels)
    {
        var registration = MapToRegistrationModel(
            registrationModel,
            orderModel,
            itemModels);

        bool isAdded = await registrationRepository.AddAsync(registration);

        return isAdded;
    }

    public async Task<RegistrationResult> GetByStudentIdAsync(Guid id)
    {
        var registration = await registrationRepository.GetByStudentIdAsync(id);

        return new RegistrationResult(registration);
    }

    private Registration MapToRegistrationModel(
        dynamic registrationModel,
        dynamic orderModel,
        List<dynamic> itemModels)
    {
        var orderItems = MapToOrderItemModels(itemModels);
        var order = MapToOrderModel(orderModel, orderItems, registrationModel.Id);

        var registration = new Registration()
        {
            Id = registrationModel.Id,
            StudentId = registrationModel.StudentId,
            CourseOfferId = registrationModel.CourseOfferId,
            Status = registrationModel.Status,
            CreatedOn = registrationModel.CreatedOn,
            CourseMaterialOrder = order
        };

        return registration;
    }

    private IEnumerable<CourseMaterialOrderItem> MapToOrderItemModels(List<dynamic> itemModels)
    {
        var orderItems = new List<CourseMaterialOrderItem>();
        foreach (var item in itemModels)
        {
            var orderItem = new CourseMaterialOrderItem()
            {
                Id = item.CourseMaterialOrderId,
                CourseMaterialId = item.CourseMaterialId,
                Quantity = item.Quantity
            };

            orderItems.Add(orderItem);
        }

        return orderItems;
    }

    private CourseMaterialOrder MapToOrderModel(
        dynamic orderModel,
        IEnumerable<CourseMaterialOrderItem> orderItems,
        Guid registrationId)
    {
        var order = new CourseMaterialOrder()
        {
            Id = orderModel.Id,
            RegistrationId = registrationId,
            OrderStatus = orderModel.OrderStatus,
            OrderDate = orderModel.OrderDate,
            CourseMaterialOrderItems = orderItems
        };

        return order;
    }
}
