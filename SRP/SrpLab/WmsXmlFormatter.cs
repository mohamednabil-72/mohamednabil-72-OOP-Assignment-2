namespace SrpLab;

public sealed class WmsXmlFormatter
{
    public string Format(string batchId, IEnumerable<(string Sku, int Allocated)> allocations)
        => $"<batch id=\"{batchId}\">{string.Join("", allocations.Select(a => $"<line sku=\"{a.Sku}\" qty=\"{a.Allocated}\" />"))}</batch>";
}
