using Microsoft.AspNetCore.Mvc;
using PlanningAssumptionsGovernance.Api.Application.Contracts;
using PlanningAssumptionsGovernance.Api.Domain;

namespace PlanningAssumptionsGovernance.Api.Controllers;

[ApiController]
[Route("api/governance")]
public sealed class GovernanceController : ControllerBase
{
    private readonly IGovernanceService _service;

    public GovernanceController(IGovernanceService service)
    {
        _service = service;
    }

    [HttpPost("submit")]
    public ActionResult<AssumptionVersion> Submit([FromBody] SubmitAssumptionRequest request)
    {
        return Ok(_service.Submit(request));
    }

    [HttpPost("approve")]
    public ActionResult<AssumptionVersion> Approve([FromBody] ApproveAssumptionRequest request)
    {
        var result = _service.ApproveOrReject(request);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpGet("assumptions")]
    public ActionResult<IReadOnlyList<AssumptionVersion>> Assumptions()
    {
        return Ok(_service.ListAssumptions());
    }

    [HttpGet("audit")]
    public ActionResult<IReadOnlyList<AuditEvent>> Audit()
    {
        return Ok(_service.ListAuditEvents());
    }
}
