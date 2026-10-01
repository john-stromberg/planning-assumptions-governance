# Planning Assumptions Governance

Workflow-oriented advisor tool for versioned planning assumptions, approvals, and audit trail review.

## Stack
- .NET 10 ASP.NET Core Web API
- Lightweight web UI

## MVP Features
- Submit versioned assumptions (e.g., inflation, return range, longevity input)
- Approve or reject submitted versions
- List assumptions by version history
- View audit events for governance actions

## Run
```bash
dotnet restore planning-assumptions-governance.slnx
dotnet run --project src/PlanningAssumptionsGovernance.Api/PlanningAssumptionsGovernance.Api.csproj
```

Open:
- `/` for UI
- API endpoints under `/api/governance/*`

## Notes
Current MVP uses in-memory storage. Production hardening should add persistent storage and role-based access.
