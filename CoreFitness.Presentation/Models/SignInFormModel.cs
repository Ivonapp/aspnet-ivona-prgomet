using System.ComponentModel.DataAnnotations;
namespace CoreFitness.Presentation.Models;

public class SignInFormModel
{


    [Display(Name = "Email", Prompt = "Enter Email Address")]                //Det som står i Placeholdern
    [DataType(DataType.EmailAddress)]                                       // DataTypen är email
    [Required(ErrorMessage = "Email Address is required.")]                 //Error meddelande som skrivs ut
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "You must enter a valid email address.")] // REGEX för email
    public string Email { get; set; } = null!;




    [Display(Name = "Password", Prompt = "Enter Password")]                //Det som står i Placeholdern
    [Required(ErrorMessage = "Please enter your password.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;



    //Error message skrivet i AuthController istället 
    public bool TermsAccepted { get; set; }

}
