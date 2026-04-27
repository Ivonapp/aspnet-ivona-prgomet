using CoreFitness.Domain.MembershipPlans;

namespace CoreFitness.Application.MembershipPlans

{
    public interface IMembershipPlanService
    {
        Task<IReadOnlyCollection<MembershipPlanDto>> GetMembershipsPlansAsync(CancellationToken ct = default);
    }
}