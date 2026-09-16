using LendingPlatform.Domain;

namespace LendingPlatform.Services;

public sealed record LoanAssessment(LoanDecision Decision, string Reason, decimal LoanToValue);
