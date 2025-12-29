public interface IOverpaymentService
{
    public Task<List<OverpaymentDto>?> GetOverpayentsForMortgageAsync(Guid mortgageId);
    public Task AddOverpaymentAsync(OverpaymentDto overpaymentDto, Guid mortgageId);
}