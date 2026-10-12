namespace SrpLab;

public sealed class GiftMessageFormatter
{
    public string Format(string fromName, IEnumerable<string> skus, decimal grandTotal)
    {
        var items = string.Join(", ", skus);
        return $"Dear friend,\nA gift from {fromName} awaits ({items}).\nTotal surprise value: {grandTotal:C}\n";
    }
}
