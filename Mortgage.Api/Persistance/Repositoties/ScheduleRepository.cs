using Microsoft.EntityFrameworkCore;

public class ScheduleRepository : IScheduleRepository
{
    private readonly AppDbcontext _dbContext;

    public ScheduleRepository(AppDbcontext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Schedule?> GetScheduleForMortgageAsync(Guid mortgageId)
    {
        var schedule = await _dbContext.Schedules
            .Where(i => i.MortgageeId == mortgageId)
            .OrderByDescending(s => s.Generation_Date)
            .Include(s => s.ScheduledPayments)
            .FirstOrDefaultAsync();

        return schedule;
    }
    
    public async Task SaveScheduleAsync(Schedule schedule)
    {
        await _dbContext.Schedules.AddAsync(schedule);
        await _dbContext.SaveChangesAsync();
    }
}