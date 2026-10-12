namespace SrpLab;

public sealed class HandoffNoteFormatter
{
    public string FormatEmptyBed(int bed) => $"Bed {bed}: empty";
    public string Format(int bed, string patient, int acuity)
    {
        var tone = acuity >= 8 ? "ESCALATE" : acuity >= 4 ? "WATCH" : "STABLE";
        return $"[HANDOFF {DateTime.UtcNow:yyyy-MM-dd}] Bed {bed} · {patient} · acuity={acuity} · {tone}";
    }
}
