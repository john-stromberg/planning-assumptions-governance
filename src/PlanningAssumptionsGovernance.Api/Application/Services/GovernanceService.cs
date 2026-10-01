using PlanningAssumptionsGovernance.Api.Application.Contracts;
using PlanningAssumptionsGovernance.Api.Domain;

namespace PlanningAssumptionsGovernance.Api.Application.Services;

public sealed class GovernanceService : IGovernanceService
{
    private readonly List<AssumptionVersion> _assumptions = [];
    private readonly List<AuditEvent> _audit = [];

    public AssumptionVersion Submit(SubmitAssumptionRequest request)
    {
        var latestVersion = _assumptions
            .Where(a => a.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase))
            .Select(a => a.VersionNumber)
            .DefaultIfEmpty(0)
            .Max();

        var assumption = new AssumptionVersion
        {
            Name = request.Name.Trim(),
            Value = request.Value.Trim(),
            Notes = request.Notes.Trim(),
            CreatedBy = request.CreatedBy.Trim(),
            VersionNumber = latestVersion + 1,
            Status = "Draft"
        };

        _assumptions.Add(assumption);
        _audit.Add(new AuditEvent
        {
            Action = "SubmitAssumption",
            Actor = assumption.CreatedBy,
            Details = $"Submitted {assumption.Name} v{assumption.VersionNumber} as Draft"
        });

        return assumption;
    }

    public AssumptionVersion? ApproveOrReject(ApproveAssumptionRequest request)
    {
        var found = _assumptions.FirstOrDefault(a => a.Id == request.AssumptionId);
        if (found is null)
        {
            return null;
        }

        found.Status = request.Approve ? "Approved" : "Rejected";
        found.ApprovedBy = request.ApprovedBy.Trim();
        found.ApprovedAtUtc = DateTimeOffset.UtcNow;

        _audit.Add(new AuditEvent
        {
            Action = request.Approve ? "ApproveAssumption" : "RejectAssumption",
            Actor = found.ApprovedBy,
            Details = $"{found.Status} {found.Name} v{found.VersionNumber}"
        });

        return found;
    }

    public IReadOnlyList<AssumptionVersion> ListAssumptions() =>
        _assumptions
            .OrderBy(a => a.Name)
            .ThenByDescending(a => a.VersionNumber)
            .ToList();

    public IReadOnlyList<AuditEvent> ListAuditEvents() =>
        _audit
            .OrderByDescending(a => a.TimestampUtc)
            .ToList();
}
