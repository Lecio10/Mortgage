using Microsoft.EntityFrameworkCore;

public class MortgageRepository : IMortgageRepository
{
    private readonly AppDbcontext _dbContext;

    public MortgageRepository(AppDbcontext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Mortgagee?> GetMortgageByIdAsync(Guid mortgageId)
    {
        return await _dbContext.Mortgages.FirstOrDefaultAsync(i => i.id == mortgageId);
    }

    public async Task AddMortgageAsync(Mortgagee mortgage)
    {
        await _dbContext.Mortgages.AddAsync(mortgage);

    }

    public async Task UpdatePostScheduleAsync(Mortgagee mortgage, Schedule schedule)
    {
        await _dbContext.Mortgages
        .Where(m => m.id == mortgage.id)
        .ExecuteUpdateAsync( setters => setters
            .SetProperty(i => i.Schedule_Based_Interest_Sum, Convert.ToDecimal(schedule.ScheduledPayments.Sum(i => i.Kwota_Odsetek)))
            .SetProperty(i => i.Schedule_Based_Total_Sum, Convert.ToDecimal(schedule.ScheduledPayments.Sum(i => i.Wysokość_Raty)))
        );
    }

    public async Task UpdatePostPaymentAsync(Mortgagee mortgage, ScheduledPayment scheduledPayment)
    {
        await _dbContext.Mortgages
        .Where(m => m.id == mortgage.id)
        .ExecuteUpdateAsync( setters => setters
            .SetProperty(i => i.Next_Instalment_Date, DateTime.Parse(mortgage.Next_Instalment_Date).AddMonths(1).ToString("yyyy-MM-dd"))
            .SetProperty(i => i.Remaining_Instalments, mortgage.Remaining_Instalments - 1)
            .SetProperty(i => i.Remainig_Loan, mortgage.Remainig_Loan - scheduledPayment.Kwota_Kapitału)
        );
    }
}