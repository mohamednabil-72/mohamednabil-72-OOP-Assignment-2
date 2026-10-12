namespace SrpLab;

public sealed class StockAllocator
{
    public int Allocate(int needed, int onHand) => Math.Min(needed, onHand);
}
