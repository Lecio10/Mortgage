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
            .Where(i => i.MortgageeId == mortgageId && i.IsActive)
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

    public async Task DisableCurrentlyActiveSchedule(Guid mortgageId)
    {
        await _dbContext.Schedules
            .Where(m => m.MortgageeId == mortgageId)
            .ExecuteUpdateAsync(m => m.SetProperty (x => x.IsActive, false));
        await _dbContext.SaveChangesAsync();
    }

    public async Task MarkSchedulePaymentAsPaidAsync(Guid schedulePaymentId)
    {
        await _dbContext.ScheduledPayments
            .Where(m => m.Id == schedulePaymentId)
            .ExecuteUpdateAsync(m => m.SetProperty (x => x.IsPaid, true));
        await _dbContext.SaveChangesAsync();
    }
}