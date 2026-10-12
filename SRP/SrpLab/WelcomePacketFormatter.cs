namespace SrpLab;

public sealed class WelcomePacketFormatter
{
    public string Format(string courseCode, string studentEmail, string studentName, bool seated, int waitlistPosition)
    {
        var status = seated ? "confirmed seat" : $"waitlist #{waitlistPosition}";
        return $"# Welcome to {courseCode}\nHi {studentName},\nYour status: **{status}**.\n" +
               $"Bring a laptop. Discord onboarding link: https://example.invalid/{courseCode.ToLowerInvariant()}\n";
    }
}
