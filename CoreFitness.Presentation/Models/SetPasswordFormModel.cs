
using System.ComponentModel.DataAnnotations;
namespace CoreFitness.Presentation.Models;

public class SetPasswordFormModel
{
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;
}
