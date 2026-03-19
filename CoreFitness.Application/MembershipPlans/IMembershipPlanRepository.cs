namespace CoreFitness.Application.MembershipPlans;

public interface IMembershipPlanRepository
{
    Task<IReadOnlyCollection<MembershipPlanDto>> GetAllWithFeaturesAsync(CancellationToken ct = default);
}
