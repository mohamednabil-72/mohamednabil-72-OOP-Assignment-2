namespace SrpLab;

public sealed class TranscriptFormatter
{
    public string Format(string studentId, string fullName, decimal average, string letter, bool honor)
        => $"TRANSCRIPT\nStudent: {fullName} ({studentId})\nAverage: {average}\nLetter: {letter}\nHonor: {honor}\n";
}
