using System.ComponentModel.DataAnnotations;

namespace CoreFitness.Presentation.Models;

public class RegisterFormModel
{
    //SKRIV DET SOM SKA VARA MED I DITT FORMULÄR I "BECOME A MEMBER"


    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email Address is required.")]

    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "You must enter a valid email address.")]

    public string Email { get; set; } = null!;
}
