using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.EntityModels;
using GermanCourseRegistration.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GermanCourseRegistration.Repositories.Implementations;

public class StudentRepository : IStudentRepository
{
    private readonly GermanCourseRegistrationDbContext dbContext;

    public StudentRepository(GermanCourseRegistrationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Student?> AddAsync(Student student)
    {
        await dbContext.Students.AddAsync(student);
        await dbContext.SaveChangesAsync();

        return student;
    }

    public async Task<Student?> GetAsync(Guid id)
    {
        return await dbContext.Students
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Student?> UpdateAsync(Student student)
    {
        var existingStudent = await dbContext.Students
            .FirstOrDefaultAsync(s => s.Id == student.Id);

        if (existingStudent != null)
        {
            existingStudent.Salutation = student.Salutation;
            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.Birthday = student.Birthday;
            existingStudent.Gender = student.Gender;
            existingStudent.Mobile = student.Mobile;
            existingStudent.Email = student.Email;
            existingStudent.Address = student.Address;
            existingStudent.PostalCode = student.PostalCode;
            existingStudent.LastModifiedOn = student.LastModifiedOn;

            await dbContext.SaveChangesAsync();

            return existingStudent;
        }

        return null;
    }
}

