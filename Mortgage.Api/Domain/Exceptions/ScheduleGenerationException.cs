public sealed class ScheduleGenerationException : Exception
{
    public Guid MortgageId { get; }
    public ScheduleGenerationException(Guid mortgageId)
        : base($"Schedule generation failed for mortgage id '{mortgageId}'.")
    {
        MortgageId = mortgageId;
    }
}