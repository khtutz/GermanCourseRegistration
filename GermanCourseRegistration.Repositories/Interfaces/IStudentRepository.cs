using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Repositories.Interfaces;

public interface IStudentRepository
{
    Task<Student?> GetAsync(Guid id);

    Task<Student?> AddAsync(Student student);

    Task<Student?> UpdateAsync(Student student);
}

