using LendingPlatform.Domain;
using LendingPlatform.Services;
using Xunit;

namespace LendingPlatform.Tests;

public sealed class LoanDecisionServiceTests
{
    private readonly LoanDecisionService _service = new();

    [Theory]
    [InlineData(99_999, 200_000, 999)]
    [InlineData(1_500_001, 2_500_000, 999)]
    public void Declines_loan_amount_outside_general_limits(decimal loanAmount, decimal assetValue, int score)
    {
        var result = _service.Assess(loanAmount, assetValue, score);
        Assert.Equal(LoanDecision.Declined, result.Decision);
    }

    [Theory]
    [InlineData(500_000, 1_000_000, 750, LoanDecision.Accepted)]
    [InlineData(500_000, 1_000_000, 749, LoanDecision.Declined)]
    [InlineData(600_000, 1_000_000, 800, LoanDecision.Accepted)]
    [InlineData(800_000, 1_000_000, 900, LoanDecision.Accepted)]
    [InlineData(900_000, 1_000_000, 999, LoanDecision.Declined)]
    public void Applies_correct_rules_below_one_million(decimal loanAmount, decimal assetValue, int score, LoanDecision expected)
    {
        var result = _service.Assess(loanAmount, assetValue, score);
        Assert.Equal(expected, result.Decision);
    }

    [Theory]
    [InlineData(1_000_000, 1_666_666.6667, 950, LoanDecision.Accepted)]
    [InlineData(1_000_000, 1_666_666.6667, 949, LoanDecision.Declined)]
    [InlineData(1_000_000, 1_500_000, 999, LoanDecision.Declined)]
    public void Applies_high_value_rules_at_one_million_and_above(decimal loanAmount, decimal assetValue, int score, LoanDecision expected)
    {
        var result = _service.Assess(loanAmount, assetValue, score);
        Assert.Equal(expected, result.Decision);
    }
}
