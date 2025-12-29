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
}