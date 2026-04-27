using CoreFitness.Domain.MembershipPlans;

namespace CoreFitness.Presentation.ViewModels;

public class MembershipViewModel
{
    public IReadOnlyCollection<MembershipPlanDto> MembershipPlans { get; set; } = [];
}
