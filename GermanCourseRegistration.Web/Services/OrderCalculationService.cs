using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Web.Services;

public class OrderCalculationService
{
    public static decimal CalculateTotalAmount(IEnumerable<CourseMaterialOrderItem> items)
    {
        decimal totalAmount = 0;

        foreach (var item in items)
        {
            var courseMaterial = item.CourseMaterial;

            if (courseMaterial != null)
            {
                decimal itemAmount = courseMaterial.Price * item.Quantity;
                totalAmount += itemAmount;
            }
        }

        return totalAmount;
    }
}
