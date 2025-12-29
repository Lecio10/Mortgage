public interface IOverpaymentRepository
{
    public Task<List<Overpayment>> GetOverpaymentsForMortgageAsync(Guid mortgageId);
    public Task AddOverpaymentAsync(Overpayment overpayment);
}