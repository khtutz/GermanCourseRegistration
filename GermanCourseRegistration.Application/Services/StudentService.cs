using AutoMapper;
using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.Application.Messages.StudentMessages;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository studentRepository;
    private readonly IMapper mapper;

    public StudentService(IStudentRepository studentRepository, IMapper mapper)
    {
        this.studentRepository = studentRepository;
        this.mapper = mapper;
    }

    public async Task<GetStudentByIdResponse> GetByIdAsync(GetStudentByIdRequest request)
    {
        var student = await studentRepository.GetByIdAsync(request.Id);

        var response = mapper.Map<GetStudentByIdResponse>(student);

        return response;
    }

    public async Task<AddStudentResponse> AddAsync(AddStudentRequest request)
    {
        var student = mapper.Map<Student>(request);

        bool isAdded = await studentRepository.AddAsync(student);

        var response = new AddStudentResponse()
        {
            IsTransactionSuccess = isAdded,
            Message = isAdded
                ? "Student information added successfully."
                : "Failed to add student information."
        };

        return response;
    }

    public async Task<UpdateStudentResponse> UpdateAsync(UpdateStudentRequest request)
    {
        var student = mapper.Map<Student>(request);

        Student? updatedStudent = await studentRepository.UpdateAsync(student, request.Id);

        var response = mapper.Map<UpdateStudentResponse>((
            updatedStudent,
            updatedStudent != null,
            updatedStudent != null
            ? "Student information updated successfully."
            : "Failed to update student information."));

        return response;
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
