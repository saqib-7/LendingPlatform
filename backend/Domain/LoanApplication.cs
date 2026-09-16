namespace LendingPlatform.Domain;

public sealed class LoanApplication
{
    public int Id { get; set; }
    public decimal LoanAmount { get; set; }
    public decimal AssetValue { get; set; }
    public int CreditScore { get; set; }
    public decimal LoanToValue { get; set; }
    public LoanDecision Decision { get; set; }
    public string DecisionReason { get; set; } = string.Empty;
    public DateTimeOffset SubmittedAtUtc { get; set; }
}
