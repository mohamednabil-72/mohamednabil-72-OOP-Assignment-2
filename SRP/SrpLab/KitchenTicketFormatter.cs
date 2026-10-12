namespace SrpLab;

public sealed class KitchenTicketFormatter
{
    public string Format(int orderNumber, IEnumerable<(string Item, int PrepMinutes)> items, int eta, IReadOnlyList<string> allergens)
    {
        const int width = 32;
        var line = new string('=', width);
        var body = string.Join('\n', items.Select(i => $"* {i.Item.ToUpperInvariant()} ({i.PrepMinutes}m)"));
        var allergyLine = allergens.Count == 0 ? "ALLERGENS: none" : "ALLERGENS: " + string.Join(",", allergens);
        return $"{line}\nORDER #{orderNumber}\nETA {eta} MIN\n{body}\n{allergyLine}\n{line}\n";
    }
}
