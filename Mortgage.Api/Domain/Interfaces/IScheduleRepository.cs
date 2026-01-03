public interface IScheduleRepository
{
    public Task<Schedule?> GetScheduleForMortgageAsync(Guid mortgageId);
    public Task SaveScheduleAsync(Schedule schedule);
    public Task DisableCurrentlyActiveSchedule(Guid mortgageId);
    public Task MarkSchedulePaymentAsPaidAsync(Guid scheduledPaymentId);
}