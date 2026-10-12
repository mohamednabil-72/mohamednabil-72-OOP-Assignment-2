namespace SrpLab;

public sealed class PickerInstructionFormatter
{
    public string Format(IEnumerable<(string Aisle, int Bin, string Sku, int Qty)> walkingOrder, IEnumerable<string> shortages)
    {
        var steps = walkingOrder.Select((s, i) => $"{i + 1}. Go aisle {s.Aisle} bin {s.Bin}: pick {s.Qty} × {s.Sku}");
        var missing = shortages.ToList();
        var warn = missing.Count > 0 ? "SHORTAGES: " + string.Join(", ", missing) : "SHORTAGES: none";
        return string.Join('\n', steps) + "\n" + warn;
    }
}
