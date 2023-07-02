using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.Application.ServiceResults;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        this.studentRepository = studentRepository;
    }

    public async Task<StudentResult> GetByIdAsync(Guid id)
    {
        var student = await studentRepository.GetByIdAsync(id);

        return new StudentResult(student);
    }

    public async Task<bool> AddAsync(
        Guid id,
        string salutation,
        string firstName,
        string lastName,
        DateTime birthday,
        string gender,
        string mobile,
        string email, 
        string address, 
        string postalCode, 
        DateTime createdOn)
    {
        var student = new Student()
        {
            Id = id,
            Salutation = salutation,
            FirstName = firstName,
            LastName = lastName,
            Birthday = birthday,
            Gender = gender,
            Mobile = mobile,
            Email = email,
            Address = address,
            PostalCode = postalCode,
            CreatedOn = createdOn
        };

        bool isAdded = await studentRepository.AddAsync(student);

        return isAdded;
    }

    public async Task<StudentResult> UpdateAsync(
        Guid id, 
        string salutation, 
        string firstName, 
        string lastName, 
        DateTime birthday, 
        string gender, 
        string mobile, 
        string email, 
        string address, 
        string postalCode, 
        DateTime lastModifiedOn)
    {
        var student = new Student()
        {
            Id = id,
            Salutation = salutation,
            FirstName = firstName,
            LastName = lastName,
            Birthday = birthday,
            Gender = gender,
            Mobile = mobile,
            Email = email,
            Address = address,
            PostalCode = postalCode,
            LastModifiedOn = lastModifiedOn
        };

        Student? updatedStudent = await studentRepository.UpdateAsync(student, id);

        return new StudentResult(updatedStudent);
    }

    public IEnumerable<string> GetSalutations()
    {
        var salutations = new List<string>()
        {
            Student.Mr,
            Student.Mrs,
            Student.Ms,
            Student.Miss,
            Student.Dr
        };

        return salutations;
    }
}
