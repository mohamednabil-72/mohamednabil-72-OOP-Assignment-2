namespace PatternsLab.Problems.Builder;

public static class AccessModes
{
    public const string LiveGroup = "LiveGroup";
    public const string VideosOnly = "VideosOnly";
}

public sealed class CourseRegistration
{
    public string StudentEmail { get; }
    public string CourseCode { get; }
    public string AccessMode { get; }
    public string? GroupCode { get; }
    public string? DiscountCode { get; }
    public bool SendWhatsApp { get; }
    public bool SendEmailWelcome { get; }
    public string? MentorNote { get; }
    public DateOnly? PreferredStart { get; }

    private CourseRegistration(Builder b)
    {
        StudentEmail = b.StudentEmail;
        CourseCode = b.CourseCode;
        AccessMode = b.AccessModeValue!;
        GroupCode = b.GroupCodeValue;
        DiscountCode = b.DiscountCodeValue;
        SendWhatsApp = b.SendWhatsAppValue;
        SendEmailWelcome = b.SendEmailWelcomeValue;
        MentorNote = b.MentorNoteValue;
        PreferredStart = b.PreferredStartValue;
    }

    public static Builder For(string studentEmail, string courseCode) => new(studentEmail, courseCode);

    public override string ToString()
        => $"{StudentEmail} → {CourseCode} [{AccessMode}] group={GroupCode ?? "-"} discount={DiscountCode ?? "-"} wa={SendWhatsApp} mail={SendEmailWelcome}";

    public sealed class Builder
    {
        internal string StudentEmail { get; }
        internal string CourseCode { get; }
        internal string? AccessModeValue { get; private set; }
        internal string? GroupCodeValue { get; private set; }
        internal string? DiscountCodeValue { get; private set; }
        internal bool SendWhatsAppValue { get; private set; }
        internal bool SendEmailWelcomeValue { get; private set; }
        internal string? MentorNoteValue { get; private set; }
        internal DateOnly? PreferredStartValue { get; private set; }

        internal Builder(string studentEmail, string courseCode)
        {
            StudentEmail = studentEmail;
            CourseCode = courseCode;
        }

        public Builder LiveGroup(string groupCode)
        {
            AccessModeValue = AccessModes.LiveGroup;
            GroupCodeValue = groupCode;
            return this;
        }

        public Builder VideosOnly()
        {
            AccessModeValue = AccessModes.VideosOnly;
            return this;
        }

        public Builder WithAccessMode(string accessMode)
        {
            AccessModeValue = accessMode;
            return this;
        }

        public Builder WithGroupCode(string? groupCode) { GroupCodeValue = groupCode; return this; }
        public Builder WithDiscount(string? discountCode) { DiscountCodeValue = discountCode; return this; }
        public Builder NotifyByWhatsApp(bool send = true) { SendWhatsAppValue = send; return this; }
        public Builder SendWelcomeEmail(bool send = true) { SendEmailWelcomeValue = send; return this; }
        public Builder WithMentorNote(string? note) { MentorNoteValue = note; return this; }
        public Builder StartingOn(DateOnly? date) { PreferredStartValue = date; return this; }

        public CourseRegistration Build()
        {
            if (string.IsNullOrWhiteSpace(StudentEmail))
                throw new ArgumentException("Student email is required.");
            if (string.IsNullOrWhiteSpace(CourseCode))
                throw new ArgumentException("Course code is required.");
            if (string.IsNullOrWhiteSpace(AccessModeValue))
                throw new InvalidOperationException("Access mode is required: call LiveGroup(groupCode) or VideosOnly().");

            switch (AccessModeValue)
            {
                case AccessModes.LiveGroup:
                    if (string.IsNullOrWhiteSpace(GroupCodeValue))
                        throw new InvalidOperationException("LiveGroup registration requires a GroupCode.");
                    break;
                case AccessModes.VideosOnly:
                    if (!string.IsNullOrWhiteSpace(GroupCodeValue))
                        throw new InvalidOperationException("VideosOnly registration cannot have a GroupCode.");
                    break;
                default:
                    throw new InvalidOperationException($"Unknown access mode '{AccessModeValue}'.");
            }

            return new CourseRegistration(this);
        }
    }
}

public static class RegistrationCallSites
{
    public static CourseRegistration CreateLiveStudent()
        => CourseRegistration
            .For("sara@mail.com", "SEF-101")
            .LiveGroup("G1")
            .WithDiscount("EARLY10")
            .NotifyByWhatsApp()
            .SendWelcomeEmail()
            .WithMentorNote("Needs evening slot")
            .StartingOn(new DateOnly(2026, 10, 1))
            .Build();

    public static CourseRegistration CreateVideosOnly()
        => CourseRegistration
            .For("ali@mail.com", "SEF-101")
            .VideosOnly()
            .SendWelcomeEmail()
            .Build();
}
