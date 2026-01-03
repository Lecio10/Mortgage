public interface IPaymentRepository
{
    public Task AddPaymentAsync(Payment payment);
}