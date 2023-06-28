using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.Application.ServiceResults;

namespace GermanCourseRegistration.Application.Services;

public class CartService : ICartService
{
    private readonly ICourseMaterialOrderItemRepository itemRepository;

    public CartService(ICourseMaterialOrderItemRepository itemRepository)
    {
        this.itemRepository = itemRepository;
    }

    public async Task<MaterialOrdersResult> GetItemsByOrderIdAsync(Guid id)
    {
        var courseMaterialOrderItems = await itemRepository.GetAllByOrderIdAsync(id);

        return new MaterialOrdersResult(courseMaterialOrderItems);
    }

    public decimal CalculateTotalAmount(MaterialOrdersResult order)
    {
        decimal totalAmount = 0;

        if (order != null)
        {
            foreach (var item in order.CourseMaterialOrderItems)
            {
                var courseMaterial = item.CourseMaterial;

                if (courseMaterial != null)
                {
                    decimal itemAmount = courseMaterial.Price * item.Quantity;
                    totalAmount += itemAmount;
                }
            }
        }
       
        return totalAmount;
    }
}
