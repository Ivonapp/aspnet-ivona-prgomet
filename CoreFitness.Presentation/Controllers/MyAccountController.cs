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
    public async Task<IActionResult> Upload(MyAccountFormModel model)
    {
        if (!ModelState.IsValid || model.File == null || model.File.Length == 0)
            return View("~/Views/Account/MyAccount.cshtml", model);

        var uploadFolder = Path.Combine(_env.WebRootPath, "Uploads");               //folder för bild
        Directory.CreateDirectory(uploadFolder);                                    //Om foldern inte finns så skapas en

        var filePath = Path.Combine(uploadFolder, Path.GetFileName(model.File.FileName)); // bILDen kunden laddar upp sparas här

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await model.File.CopyToAsync(stream);   
        }

         ViewBag.Message = "File was uploaded successfully.";




        return RedirectToAction("MyAccount");
    }



























    //MYACCOUNT SIDAN
    public IActionResult MyAccount()
    {
        return View("~/Views/Account/MyAccount.cshtml");
    }

}


