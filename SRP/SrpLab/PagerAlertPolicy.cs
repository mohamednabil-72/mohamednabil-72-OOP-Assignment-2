namespace SrpLab;

public sealed class PagerAlertPolicy
{
    public bool ShouldAlert(int acuityScore) => acuityScore >= 8;
    public string CreateAlertMessage(int bed) => $"CODE-YELLOW bed={bed} at {DateTime.UtcNow:HH:mm}";
}
