using CoreFitness.Application.MembershipPlans;
using Microsoft.Extensions.DependencyInjection;

namespace CoreFitness.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication (this IServiceCollection services)
    {
        services.AddScoped<IMembershipPlanService, MembershipPlanService>();

        return services;
    } 
}
