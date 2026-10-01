namespace PlanningAssumptionsGovernance.Api.Domain;

public sealed class AssumptionVersion
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty; // e.g., Inflation, Long-Term Return
    public string Value { get; set; } = string.Empty; // e.g., 3.0%, 6.5%
    public string Notes { get; set; } = string.Empty;
    public int VersionNumber { get; set; }
    public string Status { get; set; } = "Draft"; // Draft, Approved, Rejected
    public string CreatedBy { get; set; } = string.Empty;
    public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
    public string? ApprovedBy { get; set; }
    public DateTimeOffset? ApprovedAtUtc { get; set; }
}

public sealed class SubmitAssumptionRequest
{
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
}

public sealed class ApproveAssumptionRequest
{
    public Guid AssumptionId { get; set; }
    public string ApprovedBy { get; set; } = string.Empty;
    public bool Approve { get; set; } = true;
}

public sealed class AuditEvent
{
    public DateTimeOffset TimestampUtc { get; set; } = DateTimeOffset.UtcNow;
    public string Action { get; set; } = string.Empty;
    public string Actor { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
}
