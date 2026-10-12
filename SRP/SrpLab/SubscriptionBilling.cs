namespace SrpLab;

public sealed class SubscriptionBilling
{
    public string CustomerId { get; }
    public decimal MonthlyPrice { get; }
    public DateOnly PeriodStart { get; }
    public DateOnly PeriodEnd { get; }
    public int FailedPayments { get; private set; }
    private readonly InvoiceNumberGenerator _invoiceNumbers = new();
    private readonly DunningEmailFormatter _emailFormatter = new();
    private readonly LedgerJournalFormatter _ledgerFormatter = new();
    public SubscriptionBilling(string customerId, decimal monthlyPrice, DateOnly periodStart, DateOnly periodEnd)
    { CustomerId = customerId; MonthlyPrice = monthlyPrice; PeriodStart = periodStart; PeriodEnd = periodEnd; }
    public decimal Prorate(DateOnly activeFrom)
    {
        if (activeFrom <= PeriodStart) return MonthlyPrice;
        if (activeFrom >= PeriodEnd) return 0m;
        var totalDays = PeriodEnd.DayNumber - PeriodStart.DayNumber;
        if (totalDays <= 0) return MonthlyPrice;
        var used = PeriodEnd.DayNumber - activeFrom.DayNumber;
        return Math.Round(MonthlyPrice * used / totalDays, 2);
    }
    public string NextInvoiceNumber() => _invoiceNumbers.Next(PeriodStart);
    public void RegisterFailedPayment() => FailedPayments++;
    public string DunningEmail(string customerName, DateOnly asOf)
    {
        var amount = Prorate(PeriodStart);
        var invoice = NextInvoiceNumber();
        return _emailFormatter.Format(customerName, invoice, amount, asOf, FailedPayments);
    }
    public string LedgerJournalLine(DateOnly activeFrom)
        => _ledgerFormatter.Format(CustomerId, NextInvoiceNumber(), Prorate(activeFrom));
}
