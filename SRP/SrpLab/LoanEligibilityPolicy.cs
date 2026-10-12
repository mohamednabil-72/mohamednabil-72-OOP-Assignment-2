namespace SrpLab;

public sealed class LoanEligibilityPolicy
{
    public bool IsEligible(decimal riskScore, int creditScore) => riskScore >= 55m && creditScore >= 580;
}
