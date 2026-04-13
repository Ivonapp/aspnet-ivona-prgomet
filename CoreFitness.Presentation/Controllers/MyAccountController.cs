using System.ComponentModel.Design;
using Microsoft.AspNetCore.Mvc;
using CoreFitness.Presentation.Models;
using System.Reflection.PortableExecutable;

namespace CoreFitness.Presentation.Controllers;

public class MyAccountController(IWebHostEnvironment env) : Controller
{

private readonly IWebHostEnvironment _env = env;

                //FILUPPLADDNING
    public IActionResult Upload()
    {
        
        return View();
    }




                //FILUPPLADDNING
    [HttpPost]
    public async Task<IActionResult> Upload(MyAccountFormModel formData) //Inuti Paranteserna skriver vi FormModel som styr filuppladdning. I detta fallet finns den i MyAccountFormModel.
    {
        if (!ModelState.IsValid || formData.File == null || formData.File.Length == 0)
            return View("~/Views/Account/MyAccount.cshtml", formData);

        var uploadFolder = Path.Combine(_env.WebRootPath, "Uploads");               //folder för bild
        Directory.CreateDirectory(uploadFolder);                                    //Om foldern inte finns så skapas en

        var filePath = Path.Combine(uploadFolder, Path.GetFileName(formData.File.FileName)); // bILDen användaren laddar upp sparas här

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await formData.File.CopyToAsync(stream);   
        }

         ViewBag.Message = "File was uploaded successfully.";


        return View("~/Views/Account/MyAccount.cshtml", formData);
    }



























    //MYACCOUNT SIDAN
    public IActionResult MyAccount()
    {
        return View("~/Views/Account/MyAccount.cshtml");
    }

}


