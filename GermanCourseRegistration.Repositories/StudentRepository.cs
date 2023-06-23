using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Repositories;

public class StudentRepository
    : Repository<Student, Guid>, IStudentRepository
{
    public StudentRepository(GermanCourseRegistrationDbContext dbContext)
        : base(dbContext) { }
}
