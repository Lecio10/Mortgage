public sealed class ScheduleNotFoundException : Exception
{
    public Guid MortgageId { get; }
    public ScheduleNotFoundException(Guid mortgageId)
        : base($"Schedule for mortgage id '{mortgageId}' was not found.")
    {
        MortgageId = mortgageId;
    }
}