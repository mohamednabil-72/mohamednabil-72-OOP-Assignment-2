namespace SrpLab;

public sealed class GradeCalculator
{
    public decimal Average(IEnumerable<decimal> scores) => Math.Round(scores.Average(), 2);
}
