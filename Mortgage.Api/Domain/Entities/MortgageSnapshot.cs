public class MortgageSnapshot
{
    public Guid Id { get; set; }
    public Guid MortgageeId { get; set; }
    public decimal RemainingCapital { get; set; }
    public decimal PaidInterest { get; set; }
    public decimal PaidPrincipal { get; set; }
    public int RemainingInstalments { get; set; }
    public DateTime SnapshotDate { get; set; }
}