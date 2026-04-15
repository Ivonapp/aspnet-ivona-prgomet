
using System.ComponentModel.DataAnnotations;
namespace CoreFitness.Presentation.Models;

public class SetPasswordFormModel
{
    [Display(Name ="Password", Prompt = "Enter Password")]                //Det som står i Placeholdern
    [Required(ErrorMessage = "Please enter a password.")] 
    [DataType(DataType.Password)]
    //LÄGG TILL REGEX FÖR LÖSENORD
    public string Password { get; set; } = null!;



    [Display(Name ="Password", Prompt = "Confirm Password")]                //Det som står i Placeholdern
    [Required(ErrorMessage = "Please confirm your password.")] 
    [Compare("Password", ErrorMessage = "The passwords do not match.")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;




    //Error message skrivet i AuthController istället 
    public bool TermsAccepted { get; set; }

}
