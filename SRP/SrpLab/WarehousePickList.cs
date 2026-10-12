namespace SrpLab;

public sealed class WarehousePickList
{
    private readonly List<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> _lines = new();
    private readonly StockAllocator _allocator = new();
    private readonly WarehouseRoutePlanner _routePlanner = new();
    private readonly PickerInstructionFormatter _instructionFormatter = new();
    private readonly WmsXmlFormatter _xmlFormatter = new();
    public void AddNeed(string sku, string aisle, int bin, int qtyNeeded, int qtyOnHand) => _lines.Add((sku, aisle, bin, qtyNeeded, qtyOnHand));
    public IReadOnlyList<(string Sku, int Allocated)> Allocate()
        => _lines.Select(l => (l.Sku, _allocator.Allocate(l.QtyNeeded, l.QtyOnHand))).ToList();
    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> WalkingOrder() => _routePlanner.Plan(_lines);
    public string PickerScript()
    {
        var shortages = Allocate().Where(a => { var need = _lines.First(l => l.Sku == a.Sku).QtyNeeded; return a.Allocated < need; }).Select(s => s.Sku);
        return _instructionFormatter.Format(WalkingOrder(), shortages);
    }
    public string WmsXmlBatch(string batchId) => _xmlFormatter.Format(batchId, Allocate());
}
