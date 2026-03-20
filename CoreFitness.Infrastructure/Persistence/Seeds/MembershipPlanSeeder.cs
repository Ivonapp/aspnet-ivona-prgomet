using System.Reflection;
using Microsoft.EntityFrameworkCore;
using CoreFitness.Domain.MembershipPlans;
using CoreFitness.Infrastructure.Persistence;

namespace CoreFitness.Infrastructure.Persistence.Seeds;

public static class MembershipPlanSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
 if (await context.MembershipPlans.AnyAsync())
            return;

        var standardPlan = new MembershipPlanEntity
        {

            Id = Guid.NewGuid(),
            MembershipPlanType = MembershipPlanType.Standard,
            Title = "Standard Membership",
            Description = "With the Standard Membership, get access to our full range of gym facilities.",
            Price = 495.00m,
            Monthly = "/monthly",
            MonthlyClasses = 20,
            FreeTrial = 1,

            SortOrder = 1,
            Features =
            [
                new() { Description = "Standard Locker", SortOrder = 1},
                new() { Description = "High-energy group fitness classes", SortOrder = 2},
                new() { Description = "Motivating & supportive environment", SortOrder = 3}
            ]
        };


//PREMIUM        

        var premiumPlan = new MembershipPlanEntity
        {

            Id = Guid.NewGuid(),
            MembershipPlanType = MembershipPlanType.Premium,
            Title = "Premium Membership",
            Description = "With the Premium Membership, get access to our full range of gym facilities.",
            Price = 595.00m,
            Monthly = "/monthly",
            MonthlyClasses = 20,
            FreeTrial = 1,
            SortOrder = 2,
            Features =
                        [
                            new() { Description = "Priority Support & Premium Locker", SortOrder = 1},
                            new() { Description = "High-energy group fitness classes", SortOrder = 2},
                            new() { Description = "Motivating & supportive environment", SortOrder = 3}
                        ]
        };

        context.MembershipPlans.AddRange(standardPlan,  premiumPlan);
        await context.SaveChangesAsync();
    }
}