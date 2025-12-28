public interface IMortgageRepository
{
    public Task<Mortgagee?> GetMortgageByIdAsync(Guid mortgageId);
    public Task AddMortgage(Mortgagee mortgage);
}