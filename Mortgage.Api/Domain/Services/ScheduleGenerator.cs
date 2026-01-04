using System.Reflection.Metadata.Ecma335;

public class ScheduleGenerator : IScheduleGenerator
{
    public Schedule Generate(Mortgagee mortgage, List<Overpayment>? overpayments)
    {
        var schedule = new Schedule();

        schedule.Id = Guid.NewGuid();
        schedule.MortgageeId = mortgage.id;
        schedule.Generation_Date = DateTime.Now;
        schedule.IsActive = true;
        var remaining_Loan = mortgage.Remainig_Loan;

        var previous_Payment_Date = DateTime.Parse(mortgage.Next_Instalment_Date).AddMonths(-1);
        var annuity_Payment = Calculate_Annuity_Payment(remaining_Loan, mortgage.Remaining_Instalments, mortgage.Interest_Rate_In_Percent);

        for (int i = 1; i <= mortgage.Remaining_Instalments; i++)
        {
            schedule.Number_Of_Payments = i;

            var next_Payment_Date = previous_Payment_Date.AddMonths(1);

            //Process_Snowball(annuity_Payment, next_Payment_Date, store);
            
            var remaining_Loan_After_Overpayments = Process_Overpayments(previous_Payment_Date, next_Payment_Date, remaining_Loan, overpayments);

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
                    annuity_Payment = Calculate_Annuity_Payment(remaining_Loan, mortgage.Remaining_Instalments - i + 1, mortgage.Interest_Rate_In_Percent);
                }
            }
            
            var interest = Get_Interest_For_Period(previous_Payment_Date, next_Payment_Date, remaining_Loan, mortgage.Interest_Rate_In_Percent);
            var principal_Amount = Get_Principal_Amount(annuity_Payment, interest);
            remaining_Loan = remaining_Loan - principal_Amount;
            var monthly_Payment = annuity_Payment;

            //Handle last Instalment
            if (i == mortgage.Number_Of_Instalments)
            {
                //Remaining loan must be added to montly Payment and remaining loan must be cleared.
                monthly_Payment = monthly_Payment + remaining_Loan;
                remaining_Loan = 0;
            }

            schedule.ScheduledPayments.Add( new ScheduledPayment
            {
                Numer_Raty = i,
                Data_Płatności = next_Payment_Date.ToString("yyyy-MM-dd"),
                Wysokość_Raty = Math.Round(monthly_Payment,2),
                Kwota_Odsetek = interest,
                Kwota_Kapitału = principal_Amount,
                Pozostało_Do_Spłaty = Math.Round(remaining_Loan,2),
                Id = Guid.NewGuid(),
                IsPaid = false,
                ScheduleId = schedule.Id
            });

            previous_Payment_Date = next_Payment_Date;
        }
        return schedule;
    }

    public decimal Calculate_Annuity_Payment(decimal loan_Ammount, int instalments, decimal interest_Rate_In_Percent)
    {
        var monthly_interest_rate = interest_Rate_In_Percent / 100 / 12;
        var numerator = monthly_interest_rate * Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + monthly_interest_rate), instalments));
        var denominator = Convert.ToDecimal(Math.Pow(Convert.ToDouble(1 + monthly_interest_rate), instalments) - 1);
        var payment = loan_Ammount * (numerator / denominator);
        
        return payment;
    }

    public decimal Process_Overpayments(DateTime start_Date, DateTime end_Date, decimal remaining_Loan, List<Overpayment>? overpayments)
    {
        if (overpayments is null)
        {
            return remaining_Loan;
        }
        
        var period_overpayments = overpayments.Where(a => a.Overpayment_Date > start_Date && a.Overpayment_Date <= end_Date);
        
        foreach (var overpayment in period_overpayments)
        {
            if (Convert.ToDecimal(overpayment.Amount) >= remaining_Loan)
            {
                return 0;
            }
            
            remaining_Loan = remaining_Loan - Convert.ToDecimal(overpayment.Amount);
        }
        return remaining_Loan;
    }

    public decimal Get_Interest_For_Period(DateTime start_Date, DateTime end_Date, decimal loan_Ammount, decimal interest_Rate_In_Percent)
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

    public decimal Get_Principal_Amount(decimal annuity_Payment, decimal interest)
    {
        if (interest == 0)
        {
            return 0;
        }
        return Math.Round(annuity_Payment - interest,2);
    }
}