using Microsoft.AspNetCore.Mvc;
using CoreFitness.Application.MembershipPlans;
using CoreFitness.Presentation.ViewModels;
using CoreFitness.Presentation.Models;
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



    [HttpGet]
    [Route("customerservice")]
  public IActionResult CustomerService(CustomerServiceFormModel formData)
{
    if (ModelState.IsValid)
    {
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
