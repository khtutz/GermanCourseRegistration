using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Interfaces.Repositories;

public interface IStudentRepository
{
    Task<Student?> GetByIdAsync(Guid id);

    Task<bool> AddAsync(Student student);

    Task<Student?> UpdateAsync(Student student);
}

