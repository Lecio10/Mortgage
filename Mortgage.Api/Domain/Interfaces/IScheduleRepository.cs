public interface IScheduleRepository
{
    public Task<Schedule?> GetScheduleForMortgageAsync(Guid mortgageId);
    public Task SaveScheduleAsync(Schedule schedule);
}