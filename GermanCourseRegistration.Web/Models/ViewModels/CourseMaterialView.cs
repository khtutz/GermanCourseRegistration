using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GermanCourseRegistration.Web.Models.ViewModels;

public class CourseMaterialView
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    public string? Description { get; set; }

    [Required(ErrorMessage = "Category is required.")]
    public string Category { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    public decimal Price { get; set; }


    // Display categories
    public IEnumerable<SelectListItem>? AvailableCategories { get; set; }
}
