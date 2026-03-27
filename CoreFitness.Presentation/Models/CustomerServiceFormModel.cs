using System.ComponentModel.DataAnnotations;
namespace CoreFitness.Presentation.Models;

public class CustomerServiceFormModel
{
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName {get; set;} = null!;

    [Required(ErrorMessage = "Last name is required.")]
    public string LastName {get; set;} = null!;


    [DataType(DataType.EmailAddress)]
    [Display(Name ="Email", Prompt = "Enter Email Address")]      //Det som står i Placeholdern                        // DataTypen är email
    [Required(ErrorMessage = "Email Address is required.")]    //Error meddelande som skrivs ut
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "You must enter a valid email address.")] // REGEX för email
    public string Email {get; set;} = null!;


    [DataType(DataType.PhoneNumber)] 
    public string? PhoneNumber {get; set;}


    [Required(ErrorMessage = "Message is required.")]
    [MinLength(5, ErrorMessage = "Message must be at least 5 characters.")]
    public string Message { get; set; } = null!;
}
