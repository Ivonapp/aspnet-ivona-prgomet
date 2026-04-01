using System.ComponentModel.DataAnnotations;
// namespace CoreFitness.Presentation.Models;

    public class MyAccountFormModel
{
    
[Display(Name ="FirstName", Prompt = "Enter First Name")]                //Det som står i Placeholdern
public string FirstName { get; set; }


[Display(Name ="LastName", Prompt = "Enter Last Name")]                //Det som står i Placeholdern
public string LastName { get; set;}


[Display(Name ="Email", Prompt = "username@domain.com")]                //Det som står i Placeholdern
[DataType(DataType.EmailAddress)]                                       // DataTypen är email
[Required(ErrorMessage = "Email Address is required.")]                 //Error meddelande som skrivs ut
[RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "You must enter a valid email address.")] // REGEX för email
public string Email { get; set;}


[Display(Name ="PhoneNumber", Prompt = "Enter Phone Number")]                //Det som står i Placeholdern
public string PhoneNumber { get; set; }


}

