public sealed class ScheduledPaymentNotFoundException : Exception
{
    public Guid MortgageId { get; }
    public ScheduledPaymentNotFoundException(Guid mortgageId)
        : base($"Cannot find any avalible scheduled payments for mortgage id '{mortgageId}'.")
    {
        MortgageId = mortgageId;
    }
}