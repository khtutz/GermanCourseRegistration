using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Repositories.Interfaces;

public interface IStudentRepository
{
    Task<Student?> GetByIdAsync(Guid id);

    Task<bool> AddAsync(Student student);

    Task<Student?> UpdateAsync(Student student);
}

