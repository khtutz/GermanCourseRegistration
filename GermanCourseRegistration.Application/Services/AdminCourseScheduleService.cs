using AutoMapper;
using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.Application.Messages.CourseOfferMessages;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Services;

public class AdminCourseScheduleService : IAdminCourseScheduleService
{
    private readonly ICourseOfferRepository courseOfferRepository;
    private readonly ITimetableRepository timetableRepository;
    private readonly IMapper mapper;

    public AdminCourseScheduleService(
        ICourseOfferRepository courseOfferRepository,
        ITimetableRepository timetableRepository,
        IMapper mapper)
    {
        this.courseOfferRepository = courseOfferRepository;
        this.timetableRepository = timetableRepository;
        this.mapper = mapper;
    }

    public async Task<GetCourseOfferByIdResponse> GetByIdAsync(GetCourseOfferByIdRequest request)
    {
        var courseOffer = await courseOfferRepository.GetByIdAsync(request.Id);

        var response = mapper.Map<GetCourseOfferByIdResponse>(courseOffer);

        return response;
    }

    public async Task<GetAllCourseOffersResponse> GetAllAsync()
    {
        var courseOffers = await courseOfferRepository.GetAllAsync();

        var response = mapper.Map<GetAllCourseOffersResponse>(courseOffers);

        return response;
    }

    public async Task<AddCourseOfferResponse> AddAsync(AddCourseOfferRequest request)
    {
        var courseOffer = mapper.Map<CourseOffer>(request);
        courseOffer.Timetables = GetTimetables(
            request.DaysOfWeek,
            request.StartTimeHour,
            request.StartTimeMinute,
            request.EndTimeHour,
            request.EndTimeMinute);

        bool isAdded = await courseOfferRepository.AddAsync(courseOffer);

        var response = new AddCourseOfferResponse()
        {
            IsTransactionSuccess = isAdded,
            Message = isAdded
                ? "Course schedule added successfully."
                : "Failed to add course schedule."
        };

        return response;
    }

    public async Task<UpdateCourseOfferResponse> UpdateAsync(UpdateCourseOfferRequest request)
    {
        var courseOffer = mapper.Map<CourseOffer>(request);
        courseOffer.Timetables = GetTimetables(
            request.DaysOfWeek,
            request.StartTimeHour,
            request.StartTimeMinute,
            request.EndTimeHour,
            request.EndTimeMinute);

        CourseOffer? updatedCourseOffer = await courseOfferRepository
            .UpdateAsync(courseOffer, request.Id);

        var response = mapper.Map<UpdateCourseOfferResponse>((
            updatedCourseOffer,
            updatedCourseOffer != null,
            updatedCourseOffer != null
            ? "Course schedule updated successfully."
            : "Failed to update course schedule."));

        return response;
    }

    public async Task<DeleteCourseOfferResponse> DeleteAsync(DeleteCourseOfferRequest request)
    {
        CourseOffer? deletedCourseOffer = await courseOfferRepository.DeleteAsync(request.Id);

        var response = mapper.Map<DeleteCourseOfferResponse>((
            deletedCourseOffer,
            deletedCourseOffer != null,
            deletedCourseOffer != null
            ? "Course schedule deleted successfully."
            : "Failed to delete course material."));

        return response;
    }

    private IEnumerable<Timetable> GetTimetables(
        IEnumerable<string> daysOfWeek,
        int StartTimeHour,
        int StartTimeMinute,
        int EndTimeHour,
        int EndTimeMinute)
    {
        var timetables = new List<Timetable>();

        foreach (var day in daysOfWeek)
        {
            timetables.Add(new Timetable
            {
                DayName = day,
                StartTimeHour = StartTimeHour,
                StartTimeMinute = StartTimeMinute,
                EndTimeHour = EndTimeHour,
                EndTimeMinute = EndTimeMinute
            });
        }

        return timetables;
    }

    public IEnumerable<string> GetAvailableClassTypes()
    {
        var classTypes = new List<string>()
        {
            CourseOffer.OnlineClass,
            CourseOffer.InPersonClass
        };

        return classTypes;
    }

    public IEnumerable<string> GetDaysOfWeek()
    {
        return Timetable.DaysOfWeek;
    }
}
