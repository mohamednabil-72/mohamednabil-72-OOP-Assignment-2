namespace SrpLab;

/// <summary>Coordinates bed assignments and delegates independent responsibilities.</summary>
public sealed class WardBoard
{
    private readonly Dictionary<int, string> _bedPatient = new();
    private readonly Dictionary<int, int> _vitalsScore = new();
    private readonly AcuityScorer _acuityScorer = new();
    private readonly PagerAlertPolicy _pagerAlertPolicy = new();
    private readonly PagerLog _pagerLog = new();
    private readonly HandoffNoteFormatter _handoffNoteFormatter = new();
    private readonly CensusCsvExporter _censusCsvExporter = new();

    public void AssignBed(int bed, string patientId, int heartRate, int spo2)
    {
        if (bed <= 0) throw new ArgumentOutOfRangeException(nameof(bed));
        if (string.IsNullOrWhiteSpace(patientId)) throw new ArgumentException("patient required");
        _bedPatient[bed] = patientId.Trim().ToUpperInvariant();
        _vitalsScore[bed] = _acuityScorer.Score(heartRate, spo2);
        if (_pagerAlertPolicy.ShouldAlert(_vitalsScore[bed]))
            _pagerLog.Add(_pagerAlertPolicy.CreateAlertMessage(bed));
    }

    public int ScoreAcuity(int heartRate, int spo2) => _acuityScorer.Score(heartRate, spo2);

    public string BuildHandoffNote(int bed)
    {
        if (!_bedPatient.TryGetValue(bed, out var patient))
            return _handoffNoteFormatter.FormatEmptyBed(bed);
        return _handoffNoteFormatter.Format(bed, patient, _vitalsScore[bed]);
    }

    public IReadOnlyList<string> DrainPagerLog() => _pagerLog.Drain();
    public string ExportCensusCsv() => _censusCsvExporter.Export(_bedPatient, _vitalsScore);
}
