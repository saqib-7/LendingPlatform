using LendingPlatform.Domain;

namespace LendingPlatform.Contracts;

public sealed record LoanApplicationResponse(
    int Id,
    decimal LoanAmount,
    decimal AssetValue,
    int CreditScore,
    decimal LoanToValuePercent,
    LoanDecision Decision,
    string DecisionReason,
    DateTimeOffset SubmittedAtUtc);
