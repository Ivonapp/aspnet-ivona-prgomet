using System.ComponentModel.DataAnnotations;

namespace CoreFitness.Presentation.Models;

public class RegisterFormModel
{
    //SKRIV DET SOM SKA VARA MED I DITT FORMULÄR I "BECOME A MEMBER"

    [Display(Name ="Email", Prompt = "Enter Email Address")]                //Det som står i Placeholdern
    [DataType(DataType.EmailAddress)]                                       // DataTypen är email
    [Required(ErrorMessage = "Email Address is required.")]                 //Error meddelande som skrivs ut
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "You must enter a valid email address.")] // REGEX för email
    public string Email { get; set; } = null!;
}