using LendingPlatform.Domain;

namespace LendingPlatform.Services;

public sealed class LoanDecisionService
{
    private const decimal MinimumLoanAmount = 100_000m;
    private const decimal MaximumLoanAmount = 1_500_000m;
    private const decimal OneMillion = 1_000_000m;

    public LoanAssessment Assess(decimal loanAmount, decimal assetValue, int creditScore)
    {
        var ltv = loanAmount / assetValue * 100m;

        if (loanAmount < MinimumLoanAmount || loanAmount > MaximumLoanAmount)
            return Decline("The loan amount must be between GBP 100,000 and GBP 1,500,000 inclusive.", ltv);

        if (loanAmount >= OneMillion)
        {
            if (ltv > 60m)
                return Decline("Loans of GBP 1,000,000 or more require an LTV of 60% or less.", ltv);
            if (creditScore < 950)
                return Decline("Loans of GBP 1,000,000 or more require a credit score of at least 950.", ltv);

            return Accept(ltv);
        }

        if (ltv >= 90m)
            return Decline("Loans below GBP 1,000,000 are declined when LTV is 90% or higher.", ltv);

        var requiredScore = ltv < 60m ? 750 : ltv < 80m ? 800 : 900;
        if (creditScore < requiredScore)
            return Decline($"An LTV of {ltv:F2}% requires a credit score of at least {requiredScore}.", ltv);

        return Accept(ltv);
    }

    private static LoanAssessment Accept(decimal ltv) => new(LoanDecision.Accepted, "The application meets all lending criteria.", ltv);
    private static LoanAssessment Decline(string reason, decimal ltv) => new(LoanDecision.Declined, reason, ltv);
}
