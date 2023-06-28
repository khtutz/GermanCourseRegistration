using System.ComponentModel.DataAnnotations;

namespace GermanCourseRegistration.EntityModels;

public sealed class Student : Entity<Guid>
{
    public const string Mr = "Mr";
    public const string Mrs = "Mrs";
    public const string Miss = "Miss";
    public const string Ms = "Ms";
    public const string Dr = "Dr";

    public Student(
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
        DateTime createdOn,
        DateTime lastModifiedOn) : base(id)
    {
        Salutation = salutation;
		FirstName = firstName;
		LastName = lastName;
		Birthday = birthday;
		Gender = gender;
		Mobile = mobile;
		Email = email;
		Address = address;
		PostalCode = postalCode;
        CreatedOn = createdOn;
        LastModifiedOn = lastModifiedOn;
    }

	public Student() { }

    [StringLength(10)]
    public string Salutation { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; }

    [StringLength(50)]
    public string LastName { get; set; }

	public DateTime Birthday { get; set; }

    [StringLength(25)]
    public string Gender { get; set; }

    [StringLength(25)]
    public string Mobile { get; set; }

    [StringLength(50)]
    public string Email { get; set; }

    [StringLength(500)]
    public string Address { get; set; }

    [StringLength(10)]
    public string PostalCode { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    // Navitation properties
    public Registration? Registration { get; set; }
}
