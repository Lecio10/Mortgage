public class MortgageRateHistory
{
    public Guid Id { get; set; }
    public Guid MortgageId { get; set; }
    public Mortgagee? Mortgage { get; set; }
    public decimal InterestRate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}