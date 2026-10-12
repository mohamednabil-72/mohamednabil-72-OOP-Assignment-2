namespace SrpLab;

public sealed class CensusCsvExporter
{
    public string Export(IReadOnlyDictionary<int, string> patients, IReadOnlyDictionary<int, int> scores)
    {
        var lines = new List<string> { "bed,patient,acuity" };
        foreach (var bed in patients.Keys.OrderBy(x => x))
            lines.Add($"{bed},{patients[bed]},{scores[bed]}");
        return string.Join('\n', lines);
    }
}
