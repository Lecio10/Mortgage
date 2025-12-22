
public class Mortgagee
{
    public Guid id {get; set;}
    public string First_Instalment_Date {get; set;} = "";
    public double Loan_Ammount {get; set;}
    public double Instalments {get; set;}
    public double Interest_Rate_In_Percent {get; set;}
    public double Interest_Sum {get; set;}
    public double Total_Sum {get; set;}

    public double Calculate_Annuity_Payment()
    {   
        var monthly_interest_rate = Interest_Rate_In_Percent / 100 / 12;

        var numerator = monthly_interest_rate * Math.Pow(1 + monthly_interest_rate, Instalments);
        var denominator = Math.Pow(1 + monthly_interest_rate, Instalments) - 1;

        var payment = Loan_Ammount * (numerator / denominator);

        return payment;
    }

    public double Calculate_Annuity_Payment(double loan_Ammount, double interest_Rate_In_Percent, double instalments)
    {   
        var monthly_interest_rate = interest_Rate_In_Percent / 100 / 12;

        var numerator = monthly_interest_rate * Math.Pow(1 + monthly_interest_rate, instalments);
        var denominator = Math.Pow(1 + monthly_interest_rate, instalments) - 1;

        var payment = loan_Ammount * (numerator / denominator);

        return payment;
    }

    // public List<Payment> Generate_Schedule(OverpaymentStore store)
    // {
    //     var schedule = new Schedule().Generate(First_Instalment_Date, Loan_Ammount, Instalments, Interest_Rate_In_Percent, store);

    //     Interest_Sum = Math.Round(schedule.Sum(a => a.Kwota_Odsetek),2);
    //     Total_Sum = Math.Round(Loan_Ammount + schedule.Sum(a => a.Kwota_Odsetek),2);
        
    //     return schedule;
    // }

    public double Get_Interest_For_Period(DateTime start_Date, DateTime end_Date, double loan_Ammount, double interest_Rate_In_Percent)
    {
            var number_Of_Days = (end_Date - start_Date).Days;
            var annual_Interest_Rate = interest_Rate_In_Percent / 100;
            var interest = Math.Round(loan_Ammount * annual_Interest_Rate * number_Of_Days / 365,2);

            return interest;
    }

}