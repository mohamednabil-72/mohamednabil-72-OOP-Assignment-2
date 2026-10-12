namespace SrpLab;

public sealed class SupportTicket
{
    public string Id { get; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public DateTimeOffset OpenedAt { get; }
    public string Priority { get; private set; } = "P3";
    private readonly TicketPriorityClassifier _classifier = new();
    private readonly SlaPolicy _slaPolicy = new();
    private readonly SupportReplyFormatter _replyFormatter = new();
    public SupportTicket(string id, string subject, string body, DateTimeOffset openedAt)
    { Id = id; Subject = subject; Body = body; OpenedAt = openedAt; RecalculatePriorityFromText(); }
    public void AppendCustomerMessage(string text) { Body += "\n---\n" + text; RecalculatePriorityFromText(); }
    public void RecalculatePriorityFromText() => Priority = _classifier.Classify(Subject, Body);
    public DateTimeOffset SlaDeadline() => _slaPolicy.Deadline(OpenedAt, Priority);
    public bool IsBreached(DateTimeOffset now) => _slaPolicy.IsBreached(OpenedAt, Priority, now);
    public string DraftPublicReply(string agentName) => _replyFormatter.PublicReply(Id, Priority, agentName, SlaDeadline());
    public string InternalEscalationBlurb() => _replyFormatter.Escalation(Id, Priority, SlaDeadline());
}
