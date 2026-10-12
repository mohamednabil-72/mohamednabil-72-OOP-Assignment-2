namespace SrpLab;

public sealed class SlaPolicy
{
    public DateTimeOffset Deadline(DateTimeOffset openedAt, string priority)
        => openedAt.AddHours(priority switch { "P1" => 4, "P2" => 24, _ => 72 });
    public bool IsBreached(DateTimeOffset openedAt, string priority, DateTimeOffset now) => now > Deadline(openedAt, priority);
}
