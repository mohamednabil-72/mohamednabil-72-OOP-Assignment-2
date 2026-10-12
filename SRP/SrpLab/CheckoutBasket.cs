namespace SrpLab;

public sealed class CheckoutBasket
{
    private readonly List<(string Sku, decimal Price, int Qty)> _lines = new();
    private readonly CouponDiscountCalculator _discountCalculator = new();
    private readonly GiftMessageFormatter _giftFormatter = new();
    private readonly PaymentAuthorizationStub _paymentStub = new();
    private string? _couponRaw;
    private bool _giftWrap;

    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));
        _lines.Add((sku, price, qty));
    }
    public void ApplyCouponText(string? couponText) => _couponRaw = couponText;
    public void EnableGiftWrap() => _giftWrap = true;
    public decimal SubTotal() => _lines.Sum(l => l.Price * l.Qty);
    public decimal DiscountAmount() => _discountCalculator.Calculate(_couponRaw, SubTotal());
    public decimal GrandTotal() => Math.Max(0m, SubTotal() - DiscountAmount() + (_giftWrap ? 4.99m : 0m));
    public string GiftMessageCard(string fromName) => _giftFormatter.Format(fromName, _lines.Select(l => l.Sku), GrandTotal());
    public string AuthorizePaymentStub(string cardLast4) => _paymentStub.Authorize(GrandTotal(), cardLast4, _lines.Count);
}
