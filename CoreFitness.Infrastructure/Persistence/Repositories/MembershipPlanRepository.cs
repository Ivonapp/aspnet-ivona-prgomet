using CoreFitness.Application.MembershipPlans;
using Microsoft.EntityFrameworkCore;
using CoreFitness.Domain.MembershipPlans;

namespace CoreFitness.Infrastructure.Persistence.Repositories;

internal class MembershipPlanRepository(ApplicationDbContext context) : IMembershipPlanRepository
{
    public async Task<IReadOnlyCollection<MembershipPlanDto>> GetAllWithFeaturesAsync(CancellationToken ct = default)
    {
        return await context.MembershipPlans
            .OrderBy(x => x.SortOrder)
            .Select(x => new MembershipPlanDto(
                x.Id,
                x.MembershipPlanType,
                x.Title,
                x.Description,
                x.Features
                    .OrderBy(f => f.SortOrder)
                    .Select(f => f.Description)
                    .ToList(),
                x.Price,
                x.Monthly,
                x.MonthlyClasses,
                x.FreeTrial
            )).ToListAsync(ct);
    }
}
