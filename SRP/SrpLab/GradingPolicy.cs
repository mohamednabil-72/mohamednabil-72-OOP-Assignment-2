namespace SrpLab;

public sealed class GradingPolicy
{
    public string Letter(decimal avg) => avg >= 90 ? "A" : avg >= 80 ? "B" : avg >= 70 ? "C" : avg >= 60 ? "D" : "F";
}
