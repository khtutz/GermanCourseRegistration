using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.Application.ServiceResults;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Services;

public class AdminCourseScheduleService : IAdminCourseScheduleService
{
    private readonly ICourseOfferRepository courseOfferRepository;
    private readonly ITimetableRepository timetableRepository;

    public AdminCourseScheduleService(
        ICourseOfferRepository courseOfferRepository,
        ITimetableRepository timetableRepository)
    {
        this.courseOfferRepository = courseOfferRepository;
        this.timetableRepository = timetableRepository;
    }

    public async Task<CourseOfferResult> GetByIdAsync(Guid id)
    {
        var courseOffer = await courseOfferRepository.GetByIdAsync(id);

        return new CourseOfferResult(courseOffer);
    }

    public async Task<IEnumerable<CourseOfferResult>> GetAllAsync()
    {
        var courseOffers = await courseOfferRepository.GetAllAsync();

        var courseOfferResults = courseOffers.Select(c => new CourseOfferResult(c));

        return courseOfferResults;
    }

    public async Task<bool> AddAsync(
        Guid courseId,
        string name,
        string classType,
        decimal cost,
        DateTime startDate,
        DateTime endDate,
        Guid createdBy,
        DateTime createdOn,
        IEnumerable<string> daysOfWeek,
        int StartTimeHour,
        int StartTimeMinute,
        int EndTimeHour,
        int EndTimeMinute)
    {
        var courseOffer = new CourseOffer()
        {
            CourseId = courseId,
            Name = name,
            ClassType = classType,
            Cost = cost,
            StartDate = startDate,
            EndDate = endDate,
            CreatedBy = createdBy,
            CreatedOn = createdOn,
            Timetables = GetTimetables(
                daysOfWeek,
                StartTimeHour,
                StartTimeMinute,
                EndTimeHour,
                EndTimeMinute)
        };

        bool isAdded = await courseOfferRepository.AddAsync(courseOffer);

        return isAdded;
    }

    public async Task<CourseOfferResult> UpdateAsync(
        Guid id, 
        Guid courseId, 
        string name, 
        string classType, 
        decimal cost, 
        DateTime startDate, 
        DateTime endDate, 
        Guid lastModifiedBy, 
        DateTime lastModifiedOn,
        IEnumerable<string> daysOfWeek,
        int StartTimeHour,
        int StartTimeMinute,
        int EndTimeHour,
        int EndTimeMinute)
    {
        // Delete the existing time table first
        IEnumerable<Timetable> deletedTimetables = 
            await timetableRepository.DeleteByCouseOfferIdAsync(id);

        var courseOffer = new CourseOffer()
        {
            Id = id,
            CourseId = courseId,
            Name = name,
            ClassType = classType,
            Cost = cost,
            StartDate = startDate,
            EndDate = endDate,
            LastModifiedBy = lastModifiedBy,
            LastModifiedOn = lastModifiedOn,
            Timetables = GetTimetables(
                daysOfWeek,
                StartTimeHour,
                StartTimeMinute,
                EndTimeHour,
                EndTimeMinute)
        };

        CourseOffer? updatedCourseOffer = await courseOfferRepository
            .UpdateAsync(courseOffer, id);

        return new CourseOfferResult(updatedCourseOffer);
    }

    public async Task<CourseOfferResult> DeleteAsync(Guid id)
    {
        CourseOffer? deletedCourseOffer = await courseOfferRepository.DeleteAsync(id);

        return new CourseOfferResult(deletedCourseOffer);
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
