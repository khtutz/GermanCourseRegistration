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

    public async Task<Student?> GetByIdAsync(Guid id)
    {
        try
        {
            return await dbContext.Students
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> AddAsync(Student student)
    {
        try
        {
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            throw;
        }
    }

    public async Task<Student?> UpdateAsync(Student student)
    {
        try
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

            // To throw custom exception
        }
        catch
        {
            throw;
        }

        return null;
    }
}

