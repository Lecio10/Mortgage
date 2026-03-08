public class MortgageDto
{
    public string First_Instalment_Date {get; set;} = "";
    public decimal Loan_Ammount {get; set;}
    public int Instalments {get; set;}
    public decimal Interest_Rate_In_Percent {get; set;}
    public string? Interest_Rate_Type {get; set;}
    public decimal? First_Interest_Amount {get; set;}
}