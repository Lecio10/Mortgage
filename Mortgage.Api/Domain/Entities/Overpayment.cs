public class Overpayment
{
    public Guid Id {get; set;}
    public DateTime Overpayment_Date {get; set;}
    public double Amount {get; set;}
    public Guid MortgageeId {get; set;}

}