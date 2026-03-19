namespace CoreFitness.Application.MembershipPlans;

public class MembershipPlanService(IMembershipPlanQueries queries) : IMembershipPlanService
{
    public async Task<IReadOnlyCollection<MembershipPlanDto>> GetMembershipsPlansAsync(CancellationToken ct = default)
    {
        return await queries.GetAllWithFeaturesAsync(ct);
    }
}
