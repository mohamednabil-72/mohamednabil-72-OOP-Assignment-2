namespace SrpLab;

public sealed class GradeCsvExporter
{
    public string Export(IEnumerable<(string Id, decimal Average, string Letter, bool Honor)> records)
    {
        var rows = new List<string> { "studentId,average,letter,honor" };
        rows.AddRange(records.OrderBy(x => x.Id).Select(x => $"{x.Id},{x.Average},{x.Letter},{(x.Honor ? 1 : 0)}"));
        return string.Join('\n', rows);
    }
}
