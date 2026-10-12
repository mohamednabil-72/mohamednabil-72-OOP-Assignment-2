namespace SrpLab;

public sealed class LoanDesk
{
    public decimal RequestedAmount { get; }
    public int CreditScore { get; }
    public int EmploymentMonths { get; }
    public bool HasCollateral { get; }
    private readonly LoanRiskScorer _riskScorer = new();
    private readonly LoanEligibilityPolicy _eligibilityPolicy = new();
    private readonly LoanDecisionLetterFormatter _letterFormatter = new();
    public LoanDesk(decimal requestedAmount, int creditScore, int employmentMonths, bool hasCollateral)
    { RequestedAmount = requestedAmount; CreditScore = creditScore; EmploymentMonths = employmentMonths; HasCollateral = hasCollateral; }
    public decimal RiskScore() => _riskScorer.Score(RequestedAmount, CreditScore, EmploymentMonths, HasCollateral);
    public bool IsEligible() => _eligibilityPolicy.IsEligible(RiskScore(), CreditScore);
    public IReadOnlyList<string> RequiredDocuments()
    {
        var docs = new List<string> { "National ID", "Proof of income (3 months)" };
        if (RequestedAmount > 40_000m) docs.Add("Bank statements (6 months)");
        if (HasCollateral) docs.Add("Collateral ownership deed");
        if (EmploymentMonths < 12) docs.Add("Employer letter");
        if (!IsEligible()) docs.Add("Manual underwriter referral form");
        return docs;
    }
    public string DecisionLetter(string applicantName)
        => IsEligible() ? _letterFormatter.Approved(applicantName, RequestedAmount, RiskScore(), RequiredDocuments())
                        : _letterFormatter.Rejected(applicantName, RequestedAmount, RiskScore());
    public string UnderwriterCsvRow(string applicationId)
        => $"{applicationId},{CreditScore},{EmploymentMonths},{(HasCollateral ? 1 : 0)},{RiskScore():0.00},{(IsEligible() ? "Y" : "N")}";
}
