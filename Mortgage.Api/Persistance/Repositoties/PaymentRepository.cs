using Microsoft.EntityFrameworkCore;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbcontext _dbContext;
    public PaymentRepository(AppDbcontext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddPaymentAsync(Payment payment)
    {
        await _dbContext.Payments.AddAsync(payment);
        await _dbContext.SaveChangesAsync();
    }
}