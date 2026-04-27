namespace CoreFitness.Domain.MembershipPlans;

public interface IMembershipPlanRepository
{
    Task<IReadOnlyCollection<MembershipPlanDto>> GetAllWithFeaturesAsync(CancellationToken ct = default);
}
