namespace SrpLab;

public sealed class CourseEnrollmentDesk
{
    private readonly HashSet<string> _seated = new(StringComparer.OrdinalIgnoreCase);
    private readonly List<string> _waitlist = new();
    private readonly WelcomePacketFormatter _welcomeFormatter = new();
    private readonly TuitionInvoiceFormatter _invoiceFormatter = new();
    public int Capacity { get; }
    public decimal Tuition { get; }
    public string CourseCode { get; }

    public CourseEnrollmentDesk(string courseCode, int capacity, decimal tuition)
    { CourseCode = courseCode; Capacity = capacity; Tuition = tuition; }

    public string Register(string studentEmail)
    {
        if (string.IsNullOrWhiteSpace(studentEmail)) throw new ArgumentException("email");
        var email = studentEmail.Trim();
        if (_seated.Contains(email) || _waitlist.Contains(email)) return "ALREADY_REGISTERED";
        if (_seated.Count < Capacity) { _seated.Add(email); return "SEATED"; }
        _waitlist.Add(email); return $"WAITLIST:{_waitlist.Count}";
    }
    public int WaitlistPosition(string studentEmail)
    { var idx = _waitlist.FindIndex(x => x.Equals(studentEmail, StringComparison.OrdinalIgnoreCase)); return idx < 0 ? -1 : idx + 1; }
    public string WelcomePacketMarkdown(string studentEmail, string studentName)
        => _welcomeFormatter.Format(CourseCode, studentEmail, studentName, _seated.Contains(studentEmail), WaitlistPosition(studentEmail));
    public string TuitionInvoiceLine(string studentEmail)
        => _invoiceFormatter.Format(CourseCode, _seated.Contains(studentEmail), Tuition);
    public void PromoteFromWaitlist(int seats)
    {
        while (seats > 0 && _waitlist.Count > 0 && _seated.Count < Capacity)
        { var next = _waitlist[0]; _waitlist.RemoveAt(0); _seated.Add(next); seats--; }
    }
}
