using Microsoft.AspNetCore.Mvc;
using CoreFitness.Application.MembershipPlans;
using CoreFitness.Presentation.ViewModels;

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


    [Route("customerservice")]
    public IActionResult CustomerService()
    {
        return View();
    }



    [Route("store")]
    public IActionResult Store()
    {
        return View();
    }


}
