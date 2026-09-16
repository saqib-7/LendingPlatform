namespace LendingPlatform.Contracts;

public sealed record PortfolioSummaryResponse(
    int TotalApplications,
    int AcceptedApplications,
    int DeclinedApplications,
    decimal TotalValueOfLoansWritten,
    decimal? MeanLoanToValuePercent);
