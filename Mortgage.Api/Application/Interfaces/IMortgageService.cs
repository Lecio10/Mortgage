public interface IMortgageService
{
    public Task<MortgageDetailsDto> GetMortgageByIdAsync(Guid mortgageId);
    public Task AddMortgageAsync(MortgageDto mortgageDto);
}