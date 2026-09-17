using LendingPlatform.Contracts;
using LendingPlatform.Data;
using LendingPlatform.Domain;
using LendingPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LendingPlatform.Controllers;

[ApiController]
[Route("api/loan-applications")]
public sealed class LoanApplicationsController(LendingDbContext dbContext, LoanDecisionService loanDecisionService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<LoanApplicationResponse>> Create(CreateLoanApplicationRequest request, CancellationToken cancellationToken)
    {
        var assessment = loanDecisionService.Assess(request.LoanAmount, request.AssetValue, request.CreditScore);
        var application = new LoanApplication
        {
            LoanAmount = request.LoanAmount, AssetValue = request.AssetValue, CreditScore = request.CreditScore,
            LoanToValue = assessment.LoanToValue, Decision = assessment.Decision, DecisionReason = assessment.Reason,
            SubmittedAtUtc = DateTimeOffset.UtcNow
        };
        dbContext.LoanApplications.Add(application);
        await dbContext.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = application.Id }, ToResponse(application));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<LoanApplicationResponse>> GetById(int id, CancellationToken cancellationToken)
    {
        var application = await dbContext.LoanApplications.FindAsync([id], cancellationToken);
        return application is null ? NotFound() : Ok(ToResponse(application));
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LoanApplicationResponse>>> GetAll(
        CancellationToken cancellationToken)
    {
        var applications = await dbContext.LoanApplications
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var response = applications
            .OrderByDescending(item => item.SubmittedAtUtc)
            .Select(ToResponse)
            .ToList();

        return Ok(response);
    }

    [HttpGet("summary")]
    public async Task<ActionResult<PortfolioSummaryResponse>> GetSummary(CancellationToken cancellationToken)
    {
        // Calculate decimal aggregates in C#, not SQLite. This avoids SQLite's limited decimal aggregate support.
        var applications = await dbContext.LoanApplications.AsNoTracking().ToListAsync(cancellationToken);
        var acceptedApplications = applications.Where(item => item.Decision == LoanDecision.Accepted).ToList();

        return Ok(new PortfolioSummaryResponse(
            applications.Count,
            acceptedApplications.Count,
            applications.Count - acceptedApplications.Count,
            acceptedApplications.Sum(item => item.LoanAmount),
            applications.Count == 0 ? null : applications.Average(item => item.LoanToValue)));
    }

    private static LoanApplicationResponse ToResponse(LoanApplication application) => new(application.Id, application.LoanAmount,
        application.AssetValue, application.CreditScore, decimal.Round(application.LoanToValue, 2), application.Decision,
        application.DecisionReason, application.SubmittedAtUtc);
}
