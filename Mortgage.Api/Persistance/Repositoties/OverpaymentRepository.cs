using Microsoft.EntityFrameworkCore;

public class OverpaymentRepository : IOverpaymentRepository
{
    private readonly AppDbcontext _dbContext;
    public OverpaymentRepository(AppDbcontext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Overpayment>> GetOverpaymentsForMortgageAsync(Guid mortgageId)
    { 
       return await _dbContext.Overpayments
        .Where(i => i.MortgageeId == mortgageId)
        .ToListAsync();
    }

    public async Task AddOverpaymentAsync(Overpayment overpayment)
    {
        await _dbContext.Overpayments.AddAsync(overpayment);
        await _dbContext.SaveChangesAsync();
    }
}