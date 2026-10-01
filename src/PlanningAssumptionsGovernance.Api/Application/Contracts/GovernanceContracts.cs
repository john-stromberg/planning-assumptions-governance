using PlanningAssumptionsGovernance.Api.Domain;

namespace PlanningAssumptionsGovernance.Api.Application.Contracts;

public interface IGovernanceService
{
    AssumptionVersion Submit(SubmitAssumptionRequest request);
    AssumptionVersion? ApproveOrReject(ApproveAssumptionRequest request);
    IReadOnlyList<AssumptionVersion> ListAssumptions();
    IReadOnlyList<AuditEvent> ListAuditEvents();
}
