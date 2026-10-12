namespace SrpLab;

public sealed class HonorRollPolicy
{
    public bool IsEligible(decimal average, string letter) => average >= 85 && (letter is "A" or "B");
}
