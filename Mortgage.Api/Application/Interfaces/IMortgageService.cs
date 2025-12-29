public interface IMortgageService
{
    public Task<MortgageDto> GetMortgageByIdAsync(Guid mortgageId);
    public Task AddMortgageAsync(MortgageDto mortgageDto);
}