public interface IScheduleService
{
    public Task<ScheduleDto> GetScheduleForMortgageAsync(Guid mortgageId);
    public Task<ScheduleDto> GenerateSchedule(Guid mortgageId);
}