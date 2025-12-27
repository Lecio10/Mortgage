public class Schedule
{
    public Guid Id {get; set;}
    public Guid MortgageeId {get; set;}
    public DateTime Generation_Date {get; set;}
    public int Number_Of_Payments {get; set;}
    public ICollection<ScheduledPayment> ScheduledPayments {get; set;} = new List<ScheduledPayment>();

    public Schedule(Mortgagee mortgage)
    {
        GenerateSchedule(mortgage);
    }

    private void GenerateSchedule(Mortgagee mortgage)
    {
        Id = Guid.NewGuid();
        MortgageeId = mortgage.id;
        Generation_Date = DateTime.Now;
        
        var remaining_Loan = mortgage.Loan_Ammount;
        var previous_Payment_Date = DateTime.Parse(mortgage.First_Instalment_Date);
        var annuity_Payment = Calculate_Annuity_Payment(remaining_Loan, mortgage.Instalments, mortgage.Interest_Rate_In_Percent);

        for (int i = 1; i <= mortgage.Instalments; i++)
        {
            Number_Of_Payments = i;

            var next_Payment_Date = previous_Payment_Date.AddMonths(1);

            //Process_Snowball(annuity_Payment, next_Payment_Date, store);
            
            var remaining_Loan_After_Overpayments = Process_Overpayments(previous_Payment_Date, next_Payment_Date, remaining_Loan);

            if (remaining_Loan != remaining_Loan_After_Overpayments)
            {
                if (remaining_Loan_After_Overpayments == 0)
                {
                    //Close the loan
                    remaining_Loan = 0;
                    annuity_Payment = 0;
                }
                else
                {
                    remaining_Loan = remaining_Loan_After_Overpayments; 
                    //Recalculate annuity_Payment
                    annuity_Payment = Calculate_Annuity_Payment(remaining_Loan, mortgage.Instalments - i + 1, mortgage.Interest_Rate_In_Percent);
                }
            }
            
            var interest = Get_Interest_For_Period(previous_Payment_Date, next_Payment_Date, remaining_Loan, mortgage.Interest_Rate_In_Percent);
            var principal_Amount = Get_Principal_Amount(annuity_Payment, interest);
            remaining_Loan = remaining_Loan - principal_Amount;
            var monthly_Payment = annuity_Payment;

            //Handle last Instalment
            if (i == mortgage.Instalments)
            {
                //Remaining loan must be added to montly Payment and remaining loan must be cleared.
                monthly_Payment = monthly_Payment + remaining_Loan;
                remaining_Loan = 0;
            }

            ScheduledPayments.Add( new ScheduledPayment
            {
                Numer_Raty = i,
                Data_Płatności = next_Payment_Date.ToString("yyyy-MM-dd"),
                Wysokość_Raty = Math.Round(monthly_Payment,2),
                Kwota_Odsetek = interest,
                Kwota_Kapitału = principal_Amount,
                Pozostało_Do_Spłaty = Math.Round(remaining_Loan,2),
                Id = Guid.NewGuid(),
                ScheduleId = Id
            });

            previous_Payment_Date = next_Payment_Date;
        }
        
    }

    public double Calculate_Annuity_Payment(double loan_Ammount, double instalments, double interest_Rate_In_Percent)
    {
        var monthly_interest_rate = interest_Rate_In_Percent / 100 / 12;
        var numerator = monthly_interest_rate * Math.Pow(1 + monthly_interest_rate, instalments);
        var denominator = Math.Pow(1 + monthly_interest_rate, instalments) - 1;
        var payment = loan_Ammount * (numerator / denominator);
        
        return payment;
    }

    public double Process_Overpayments(DateTime start_Date, DateTime end_Date, double remaining_Loan)
    {
        var overpayments = new List<Overpayment>();
        var period_overpayments = overpayments.Where(a => a.Overpayment_Date > start_Date && a.Overpayment_Date <= end_Date);
        
        foreach (var overpayment in period_overpayments)
        {
            if (overpayment.Amount >= remaining_Loan)
            {
                return 0;
            }
            
            remaining_Loan = remaining_Loan - overpayment.Amount;
        }
        return remaining_Loan;
    }

    public double Get_Interest_For_Period(DateTime start_Date, DateTime end_Date, double loan_Ammount, double interest_Rate_In_Percent)
    {
        if (loan_Ammount == 0)
        {
            return 0;
        }

        var number_Of_Days = (end_Date - start_Date).Days;
        var annual_Interest_Rate = interest_Rate_In_Percent / 100;
        var interest = Math.Round(loan_Ammount * annual_Interest_Rate * number_Of_Days / 365,2);

        return Math.Round(interest,2);
    }

    public double Get_Principal_Amount(double annuity_Payment, double interest)
    {
        if (interest == 0)
        {
            return 0;
        }
        return Math.Round(annuity_Payment - interest,2);
    }

}