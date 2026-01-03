public interface IPaymentService
{
    public Task ProcessPaymentAsync(Guid mortgageId, PaymentDto paymentDto);
}