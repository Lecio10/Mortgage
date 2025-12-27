using Microsoft.EntityFrameworkCore;

public class AppDbcontext : DbContext
{
    public DbSet<Mortgagee> Mortgages { get; set; }
    public DbSet<ScheduledPayment> ScheduledPayments {get; set;}
    public DbSet<Payment> Payments {get; set;}
    public DbSet<Overpayment> Overpayments {get; set;}
    public DbSet<MortgageSnapshot> mortgageSnapshots {get; set;}
    public DbSet<MortgageRateHistory> mortgageRateHistories {get; set;}

    public DbSet<Schedule> Schedules {get; set;}
    public AppDbcontext(DbContextOptions<AppDbcontext> options)
        : base(options)
    {
        
    }
}