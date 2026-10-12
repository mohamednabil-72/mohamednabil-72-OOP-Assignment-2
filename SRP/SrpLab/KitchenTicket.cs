namespace SrpLab;

public sealed class KitchenTicket
{
    private readonly List<(string Item, List<string> Ingredients, int PrepMinutes)> _items = new();
    private readonly AllergenDetector _allergenDetector = new();
    private readonly KitchenTicketFormatter _formatter = new();
    public void AddItem(string item, IEnumerable<string> ingredients, int prepMinutes)
        => _items.Add((item, ingredients.Select(i => i.Trim().ToLowerInvariant()).ToList(), prepMinutes));
    public IReadOnlyList<string> DetectAllergens() => _allergenDetector.Detect(_items.SelectMany(i => i.Ingredients));
    public int EstimatedReadyMinutes(int openStations)
    {
        if (openStations <= 0) openStations = 1;
        var sequential = _items.Sum(i => i.PrepMinutes);
        var parallel = (int)Math.Ceiling(sequential / (double)openStations);
        if (DetectAllergens().Count > 0) parallel += 3;
        var longest = _items.Count == 0 ? 0 : _items.Max(i => i.PrepMinutes);
        return Math.Max(parallel, longest);
    }
    public string RenderThermalTicket(int orderNumber)
        => _formatter.Format(orderNumber, _items.Select(i => (i.Item, i.PrepMinutes)), EstimatedReadyMinutes(2), DetectAllergens());
    public string ExpoLaneHint() => DetectAllergens().Count > 0 ? "LANE-ALLERGY" : EstimatedReadyMinutes(2) > 20 ? "LANE-SLOW" : "LANE-FAST";
}
