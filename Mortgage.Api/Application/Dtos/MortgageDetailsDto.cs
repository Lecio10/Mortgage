public class MortgageDetailsDto
{
    public decimal Loan_Amount {get; set;}
    public decimal Interest_Rate_In_Percent {get; set;}
    public decimal Number_Of_Instalments {get; set;}
    public string Next_Instalment_Date {get; set;} = "";
    public decimal Remaining_Loan {get; set;}
    public int Remaining_Instalments {get; set;}
    public decimal Interest_Sum {get; set;}
    public decimal Total_Sum {get; set;}
}