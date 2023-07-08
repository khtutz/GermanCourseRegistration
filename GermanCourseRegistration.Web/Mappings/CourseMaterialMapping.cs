using GermanCourseRegistration.Application.Messages.CourseMaterialMessages;
using GermanCourseRegistration.Web.Models.ViewModels;

namespace GermanCourseRegistration.Web.Mappings;

public static class CourseMaterialMapping
{
    public static IEnumerable<CourseMaterialView> MapToViewModels(
        GetAllCourseMaterialsResponse response)
    {
        var viewModels = new List<CourseMaterialView>();

        foreach (var courseMaterial in response.CourseMaterials)
        {
            viewModels.Add(new CourseMaterialView()
            {
                Id = courseMaterial.Id,
                Name = courseMaterial.Name,
                Description = courseMaterial.Description,
                Category = courseMaterial.Category,
                Price = courseMaterial.Price
            });
        }

        return viewModels;
    }

    public static CourseMaterialView MapToViewModel(
        GetCourseMaterialByIdResponse response)
    {
        if (response == null)
        {
            throw new ArgumentNullException(
                nameof(response), 
                "The response object cannot be null.");
        }

        if (response.CourseMaterial == null)
        {
            throw new ArgumentNullException(
                nameof(response.CourseMaterial), 
                "The CourseMaterial property cannot be null.");
        }

        var viewModel = new CourseMaterialView()
        {
            Id = response.CourseMaterial.Id,
            Name = response.CourseMaterial.Name,
            Description = response.CourseMaterial.Description,
            Category = response.CourseMaterial.Category,
            Price = response.CourseMaterial.Price
        };

        return viewModel;
    }

    public static AddCourseMaterialRequest MapToAddRequest(
        CourseMaterialView viewModel, Guid createdBy, DateTime createdDT)
    {
        var request = new AddCourseMaterialRequest(
            viewModel.Name,
            viewModel.Description,
            viewModel.Category,
            viewModel.Price,
            createdBy,
            createdDT);

        return request;
    }

    public static UpdateCourseMaterialRequest MapToUpdateRequest(
        CourseMaterialView viewModel, Guid lastModifiedBy, DateTime lastModifiedOn)
    {
        var request = new UpdateCourseMaterialRequest(
            viewModel.Id,
            viewModel.Name,
            viewModel.Description,
            viewModel.Category,
            viewModel.Price,
            lastModifiedBy,
            lastModifiedOn);

        return request;
    }
}
