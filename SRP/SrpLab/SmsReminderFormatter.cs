namespace SrpLab;

public sealed class SmsReminderFormatter
{
    public string Format(DateTimeOffset slot, string clinicPhone)
        => $"Reminder: appointment {slot:MMM dd HH:mm}. Call {clinicPhone} to reschedule.";
}
