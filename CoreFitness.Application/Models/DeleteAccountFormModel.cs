using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreFitness.Application.Models;

public class DeleteAccountFormModel
{

    [Display(Name = "Password", Prompt = "Enter Password")]                //Det som står i Placeholdern
    [Required(ErrorMessage = "Please enter a password.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;



    [Display(Name = "Password", Prompt = "Confirm Password")]                //Det som står i Placeholdern
    [Required(ErrorMessage = "Please confirm your password.")]
    [Compare("Password", ErrorMessage = "The passwords do not match.")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;




    // **Skapa en modelstate i AccountController för error message istället för här ** 
    public bool ConfirmDelete { get; set; }
}
