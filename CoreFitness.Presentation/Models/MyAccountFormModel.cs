using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CoreFitness.Presentation.Models;
public class MyAccountFormModel
{
    
    [Display(Name ="FirstName", Prompt = "Enter First Name")]                //Det som står i Placeholdern
    public string FirstName { get; set; } = null!;


    [Display(Name ="LastName", Prompt = "Enter Last Name")]                //Det som står i Placeholdern
    public string LastName { get; set; } = null!;


    [Display(Name = "Email", Prompt = "username@domain.com")]                //Det som står i Placeholdern
    [DataType(DataType.EmailAddress)]                                       // DataTypen är email
    [Required(ErrorMessage = "Email Address is required.")]                 //Error meddelande som skrivs ut
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "You must enter a valid email address.")] // REGEX för email
    public string Email { get; set; } = null!;


    [Display(Name ="PhoneNumber", Prompt = "Enter Phone Number")]                //Det som står i Placeholdern
    public string? PhoneNumber { get; set; }






    // NEDAN FÖR BILDFIL SOM KUND SKA LÄGGA TILL
    // För uppladdning (från formuläret)
    public IFormFile? File { get; set; }


    // För att visa bilden (från databasen)
    public string? ProfileImageUrl { get; set; }
}

