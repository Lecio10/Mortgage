using Microsoft.Identity.Client;

public class Payment
{
    public Guid Id {get; set;}
    public DateTime PaymentDate {get; set;}
    public Guid ScheduledPaymentId {get; set;}
}