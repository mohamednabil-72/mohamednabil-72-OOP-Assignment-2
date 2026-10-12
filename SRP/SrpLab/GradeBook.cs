namespace SrpLab;

public sealed class GradeBook
{
    private readonly Dictionary<string, List<decimal>> _scores = new(StringComparer.OrdinalIgnoreCase);
    private readonly GradeCalculator _calculator = new();
    private readonly GradingPolicy _gradingPolicy = new();
    private readonly HonorRollPolicy _honorPolicy = new();
    private readonly TranscriptFormatter _transcriptFormatter = new();
    private readonly GradeCsvExporter _csvExporter = new();
    public void Record(string studentId, decimal score)
    {
        if (score is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(score));
        if (!_scores.TryGetValue(studentId, out var list)) { list = new List<decimal>(); _scores[studentId] = list; }
        list.Add(score);
    }
    public decimal Average(string studentId)
        => !_scores.TryGetValue(studentId, out var list) || list.Count == 0 ? 0m : _calculator.Average(list);
    public string Letter(string studentId) => _gradingPolicy.Letter(Average(studentId));
    public bool MeetsHonorRoll(string studentId) => _honorPolicy.IsEligible(Average(studentId), Letter(studentId));
    public string TranscriptPlain(string studentId, string fullName)
        => _transcriptFormatter.Format(studentId, fullName, Average(studentId), Letter(studentId), MeetsHonorRoll(studentId));
    public string ExportCsv() => _csvExporter.Export(_scores.Keys.Select(id => (id, Average(id), Letter(id), MeetsHonorRoll(id))));
}
