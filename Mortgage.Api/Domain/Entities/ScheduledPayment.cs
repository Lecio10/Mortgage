public class ScheduledPayment
{
    public Guid Id {get; set;}
    public int Numer_Raty {get; set;}
    public string? Data_Płatności {get; set;}
    public decimal Wysokość_Raty {get; set;}
    public decimal Kwota_Odsetek {get; set;}
    public decimal Kwota_Kapitału {get; set;}
    public decimal Pozostało_Do_Spłaty {get; set;}
    public Guid ScheduleId {get; set;}
    public bool IsPaid {get; set;}
    public Schedule? Schedule { get; set; }
}