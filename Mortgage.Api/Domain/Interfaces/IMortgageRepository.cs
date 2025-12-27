public interface IMortgageRepository
{
    public Task<Mortgagee?> GetMortgageByIdAsync(Guid mortgageId);
}