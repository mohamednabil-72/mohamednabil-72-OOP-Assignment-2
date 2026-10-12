namespace SrpLab;

public sealed class WarehouseRoutePlanner
{
    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> Plan(IEnumerable<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> lines)
        => lines.OrderBy(l => l.Aisle).ThenBy(l => l.Bin)
            .Select(l => (l.Aisle, l.Bin, l.Sku, Math.Min(l.QtyNeeded, l.QtyOnHand)))
            .Where(x => x.Item4 > 0).ToList();
}
