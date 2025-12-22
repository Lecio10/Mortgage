public class ScheduledPaymentDto
{
    public Guid Id {get; set;}
    public int Numer_Raty {get; set;}
    public string? Data_Płatności {get; set;}
    public double Wysokość_Raty {get; set;}
    public double Kwota_Odsetek {get; set;}
    public double Kwota_Kapitału {get; set;}
    public double Pozostało_Do_Spłaty {get; set;}
    public Guid ScheduleId {get; set;}
}