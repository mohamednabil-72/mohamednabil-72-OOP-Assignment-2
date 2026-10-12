namespace SrpLab;

public sealed class LoanDecisionLetterFormatter
{
    public string Approved(string name, decimal amount, decimal risk, IReadOnlyList<string> docs)
        => $"Dear {name},\nYour request for {amount:C} is pre-approved (risk {risk:0}).\nPlease upload: {string.Join("; ", docs)}.\n";
    public string Rejected(string name, decimal amount, decimal risk)
        => $"Dear {name},\nWe are unable to approve {amount:C} at this time.\nReference risk={risk:0}. You may reapply after improving documentation.\n";
}
