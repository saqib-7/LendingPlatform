namespace LendingPlatform.Services;

public interface ILoanDecisionService
{
    LoanAssessment Assess(decimal loanAmount, decimal assetValue, int creditScore);
}
