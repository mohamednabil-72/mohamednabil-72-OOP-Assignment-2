namespace SrpLab;

public sealed class InvoiceNumberGenerator
{
    private static int _invoiceSeq = 1000;
    public string Next(DateOnly periodStart) => $"INV-{periodStart:yyyyMM}-{++_invoiceSeq:D5}";
}
