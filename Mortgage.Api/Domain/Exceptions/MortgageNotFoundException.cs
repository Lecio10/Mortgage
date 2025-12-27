public sealed class MortgageNotFoundException : Exception
{
    public Guid MortgageId { get; }
    public MortgageNotFoundException(Guid mortgageId)
        : base($"Mortgage with id '{mortgageId}' was not found.")
    {
        MortgageId = mortgageId;
    }
}