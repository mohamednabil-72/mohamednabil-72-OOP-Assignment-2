namespace SrpLab;

public sealed class AppointmentDesk
{
    private readonly HashSet<DateTimeOffset> _booked = new();
    private readonly ClinicHoursPolicy _hoursPolicy = new();
    private readonly IcsCalendarFormatter _icsFormatter = new();
    private readonly SmsReminderFormatter _smsFormatter = new();
    public TimeOnly Open { get; }
    public TimeOnly Close { get; }
    public int SlotMinutes { get; }

    public AppointmentDesk(TimeOnly open, TimeOnly close, int slotMinutes)
    {
        Open = open; Close = close; SlotMinutes = slotMinutes;
    }

    public bool IsWithinBusinessHours(DateTimeOffset when)
        => _hoursPolicy.IsWithinBusinessHours(when, Open, Close, SlotMinutes);

    public DateTimeOffset? FindNextSlot(DateTimeOffset from, int searchHours)
    {
        var cursor = Align(from);
        var end = from.AddHours(searchHours);
        while (cursor < end)
        {
            if (IsWithinBusinessHours(cursor) && !_booked.Contains(cursor)) return cursor;
            cursor = cursor.AddMinutes(SlotMinutes);
        }
        return null;
    }

    public bool TryBook(DateTimeOffset slot)
    {
        if (!IsWithinBusinessHours(slot) || _booked.Contains(slot)) return false;
        _booked.Add(slot); return true;
    }

    public string ToIcs(DateTimeOffset slot, string patientName, string clinician)
        => _icsFormatter.Format(slot, SlotMinutes, patientName, clinician);

    public string SmsReminder(DateTimeOffset slot, string clinicPhone)
        => _smsFormatter.Format(slot, clinicPhone);

    private DateTimeOffset Align(DateTimeOffset from)
    {
        var minutes = from.Minute - (from.Minute % SlotMinutes);
        return new DateTimeOffset(from.Year, from.Month, from.Day, from.Hour, minutes, 0, from.Offset);
    }
}
