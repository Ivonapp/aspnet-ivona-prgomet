using CoreFitness.Domain.MembershipPlans;

namespace CoreFitness.Application.MembershipPlans;

public class MembershipPlanService(IMembershipPlanRepository repository) : IMembershipPlanService
{
    public async Task<IReadOnlyCollection<MembershipPlanDto>> GetMembershipsPlansAsync(CancellationToken ct = default)
    {
        return await repository.GetAllWithFeaturesAsync(ct);
    }
}
