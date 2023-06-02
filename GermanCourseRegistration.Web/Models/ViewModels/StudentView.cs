using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GermanCourseRegistration.Web.Models.ViewModels;

public class StudentView
{
    public StudentView(
        Guid id,
        string salutation,
        string firstName,
        string lastName,
        DateTime birthday,
        string gender,
        string mobile,
        string email,
        string address,
        string postalCode)
    {
		Id = id;
        Salutation = salutation;
        FirstName = firstName;
        LastName = lastName;
        Birthday = birthday;
        Gender = gender;
        Mobile = mobile;
        Email = email;
        Address = address;
        PostalCode = postalCode;
    }

	public StudentView() { }

	public Guid Id { get; set; }

	[Required(ErrorMessage = "Salutation is required.")]
	public string Salutation { get; set; }

	[Required(ErrorMessage = "First name is required.")]
	public string FirstName { get; set; }

	[Required(ErrorMessage = "Last name is required.")]
	public string LastName { get; set; }

	[Required(ErrorMessage = "Birthday is required.")]
	public DateTime Birthday { get; set; }

	[Required(ErrorMessage = "Gender is required.")]
	public string Gender { get; set; }

	[Required(ErrorMessage = "Mobile number is required.")]
	[RegularExpression(@"^\+?[0-9]+$", ErrorMessage = "Invalid mobile number.")]
	public string Mobile { get; set; }

	[Required(ErrorMessage = "Email address is required.")]
	[EmailAddress(ErrorMessage = "Invalid email address.")]
	public string Email { get; set; }

	[Required(ErrorMessage = "Address is required.")]
	public string Address { get; set; }

	[Required(ErrorMessage = "Postal code is required.")]
	public string PostalCode { get; set; }

	// Display items
	public IEnumerable<SelectListItem>? AvailableSalutations { get; set; }
	public string[]? AvailableGenders { get; set; } = { "Male", "Female", "Others" };
	public bool IsExistingStudent { get; set; }
}
