public interface IMortgageService
{
    public Task<MortgageDto> GetMortgageByIdAsync(Guid mortgageId);
    public Task AddMortgage(MortgageDto mortgageDto);
}