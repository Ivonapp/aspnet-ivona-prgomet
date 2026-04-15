using System.ComponentModel.DataAnnotations;
namespace CoreFitness.Application.Models;

public class CustomerServiceFormModel
{

    [Display(Name ="FirstName", Prompt = "Enter First Name")]      //Det som står i Placeholdern 
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName {get; set;} = null!;



    [Display(Name ="LastName", Prompt = "Enter Last Name")]      //Det som står i Placeholdern
    [Required(ErrorMessage = "Last name is required.")]
    public string LastName {get; set;} = null!;


    [DataType(DataType.EmailAddress)]                           // DataTypen är email
    [Display(Name ="Email", Prompt = "Enter Email Address")]    //Det som står i Placeholdern                        
    [Required(ErrorMessage = "Email Address is required.")]     //Error meddelande som skrivs ut
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "You must enter a valid email address.")] // REGEX för email
    public string Email {get; set;} = null!;



    [Display(Name ="Phone", Prompt = "Enter Phone Number")]             //Det som står i Placeholdern  
    [DataType(DataType.PhoneNumber)] 
    public string? PhoneNumber {get; set;}


    [Display(Name ="Text", Prompt = "Message...")]              //Det som står i Placeholdern  
    [Required(ErrorMessage = "Message is required.")]
    [MinLength(5, ErrorMessage = "Message must be at least 5 characters.")]
    public string Message { get; set; } = null!;
}
