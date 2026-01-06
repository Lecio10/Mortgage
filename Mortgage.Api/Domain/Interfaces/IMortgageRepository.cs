public interface IMortgageRepository
{
    public Task<Mortgagee?> GetMortgageByIdAsync(Guid mortgageId);
    public Task AddMortgageAsync(Mortgagee mortgage);
    public Task UpdatePostScheduleAsync(Mortgagee mortgagee, Schedule schedule);
    public Task UpdatePostPaymentAsync(Mortgagee mortgage, ScheduledPayment scheduledPayment);
}