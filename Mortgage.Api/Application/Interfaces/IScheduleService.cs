public interface IScheduleService
{
    public Task<ScheduleDto> GenerateSchedule(Guid mortgageId);
}