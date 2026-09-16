using System.ComponentModel.DataAnnotations;

namespace LendingPlatform.Contracts;

public sealed class CreateLoanApplicationRequest
{
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    public decimal LoanAmount { get; init; }

    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    public decimal AssetValue { get; init; }

    [Range(1, 999)]
    public int CreditScore { get; init; }
}
