public class Schedule
{
    public Guid Id {get; set;}
    public Guid MortgageeId {get; set;}
    public DateTime Generation_Date {get; set;}
    public int Number_Of_Payments {get; set;}
    public ICollection<ScheduledPayment> ScheduledPayments {get; set;} = new List<ScheduledPayment>();

}