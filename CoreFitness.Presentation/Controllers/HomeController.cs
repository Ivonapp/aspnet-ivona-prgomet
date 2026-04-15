using Microsoft.AspNetCore.Mvc;
using CoreFitness.Application.MembershipPlans;
using CoreFitness.Presentation.ViewModels;
using CoreFitness.Application.Models;
namespace CoreFitness.Controllers;

public class HomeController(IMembershipPlanService membershipPlanService) : Controller
{



public async Task<IActionResult> Index()
{
    var vm = new MembershipViewModel
    {
        MembershipPlans = await membershipPlanService.GetMembershipsPlansAsync()
    };
    return View(vm);
}




[Route("memberships")]
public async Task<IActionResult> Memberships()
{
    var vm = new MembershipViewModel
    {
        MembershipPlans = await membershipPlanService.GetMembershipsPlansAsync()
    };
    return View(vm);
}



    [Route("training")]
    public IActionResult Training()
    {
        return View();
    }



    [Route("fitnesscenters")]
    public IActionResult FitnessCenters()
    {
        return View();
    }







                            //CUSTOMER SERVICE
    [HttpGet]                        // GET laddar fram den vanliga sidan bara
    [Route("customerservice")]
    public IActionResult CustomerService()          // Denna behövs för att regex inte ska lysa rött
    {
        var model = new CustomerServiceFormModel();
        return View(model);
    }


    [HttpPost]
    [Route("customerservice")]
    public IActionResult CustomerService(CustomerServiceFormModel formData)
    {
        if (ModelState.IsValid)
        {
            TempData["AlertSuccessTitle"] = "Thank you!";
            TempData["AlertSuccessMessage"] = "We will get back to you within 72 hours.";
            return RedirectToAction("CustomerService");
        }

        return View(formData);
    }














    [Route("store")]
    public IActionResult Store()
    {
        return View();
    }


}
