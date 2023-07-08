using AutoMapper;
using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.Application.Messages.CourseMaterialMessages;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Services;

public class AdminCourseMaterialService : IAdminCourseMaterialService
{
    private readonly ICourseMaterialRepository courseMaterialRepository;
    private readonly IMapper mapper;

    public AdminCourseMaterialService(
        ICourseMaterialRepository courseMaterialRepository,
        IMapper mapper)
    {
        this.courseMaterialRepository = courseMaterialRepository;
        this.mapper = mapper;
    }

    public async Task<GetCourseMaterialByIdResponse> GetByIdAsync(
        GetCourseMaterialByIdRequest request)
    {
        var courseMaterial = await courseMaterialRepository.GetByIdAsync(request.Id);

        var response = mapper.Map<GetCourseMaterialByIdResponse>(courseMaterial);

        return response;
    }

    public async Task<GetAllCourseMaterialsResponse> GetAllAsync()
    {
        var courseMaterials = await courseMaterialRepository.GetAllAsync();

        var responses = mapper.Map<GetAllCourseMaterialsResponse>(courseMaterials);

        return responses;
    }

    public async Task<AddCourseMaterialResponse> AddAsync(AddCourseMaterialRequest request)
    {
        var courseMaterial = mapper.Map<CourseMaterial>(request);

        bool isAdded = await courseMaterialRepository.AddAsync(courseMaterial);

        var response = new AddCourseMaterialResponse()
        {
            IsTransactionSuccess = isAdded,
            Message = isAdded 
                ? "Course material added successfully."
                : "Failed to add course material."
        };

        return response;
    }

    public async Task<UpdateCourseMaterialResponse> UpdateAsync(UpdateCourseMaterialRequest request)
    {
        var courseMaterial = mapper.Map<CourseMaterial>(request);

        CourseMaterial? updatedCourseMaterial = 
            await courseMaterialRepository.UpdateAsync(courseMaterial, request.Id);

        var response = mapper.Map<UpdateCourseMaterialResponse>((
            updatedCourseMaterial,
            updatedCourseMaterial != null,
            updatedCourseMaterial != null
            ? "Course material updated successfully."
            : "Failed to update course material."));

        return response;
    }

    public async Task<DeleteCourseMaterialResponse> DeleteAsync(
        DeleteCourseMaterialRequest request)
    {
        CourseMaterial? deletedCourseMaterial = 
            await courseMaterialRepository.DeleteAsync(request.Id);

        var response = mapper.Map<DeleteCourseMaterialResponse>((
            deletedCourseMaterial, 
            deletedCourseMaterial != null,
            deletedCourseMaterial != null
            ? "Course material deleted successfully."
            : "Failed to delete course material."));

        return response;
    }

    public IEnumerable<string> GetCourseMaterialCategories()
    {
        var categories = new List<string>()
        {
            CourseMaterial.PhysicalCopyCategory,
            CourseMaterial.DigitalCopyCategory,
            CourseMaterial.AudioCopyCategory
        };

        return categories;
    }
}
