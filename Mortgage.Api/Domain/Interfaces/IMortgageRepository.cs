public interface IMortgageRepository
{
    public Task<Mortgagee?> GetMortgageByIdAsync(Guid mortgageId);
    public Task AddMortgageAsync(Mortgagee mortgage);
}