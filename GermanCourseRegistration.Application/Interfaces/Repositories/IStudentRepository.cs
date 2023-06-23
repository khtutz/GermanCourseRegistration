using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Interfaces.Repositories;

public interface IStudentRepository : IRepository<Student, Guid> { }