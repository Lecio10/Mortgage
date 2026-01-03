public class PaymentService : IPaymentService
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IMortgageRepository _mortgageRepository;
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IScheduleRepository scheduleRepository, IMortgageRepository mortgageRepository, IPaymentRepository paymentRepository)
    {
        _scheduleRepository = scheduleRepository;
        _mortgageRepository = mortgageRepository;
        _paymentRepository = paymentRepository;
    }
    public async Task ProcessPaymentAsync(Guid mortgageId, PaymentDto paymentDto)
    {
        
        var mortgage = await _mortgageRepository.GetMortgageByIdAsync(mortgageId);
        
        if (mortgage is null)
        {
            throw new MortgageNotFoundException(mortgageId);
        }

        var schedule = await _scheduleRepository.GetScheduleForMortgageAsync(mortgageId);
        
        if (schedule is null)
        {
            throw new ScheduleNotFoundException(mortgageId);
        }

        var schedulePayment = schedule.ScheduledPayments.OrderBy(i => i.Numer_Raty).FirstOrDefault(i => i.IsPaid == false);

        if (schedulePayment is null)
        {
            throw new ScheduledPaymentNotFoundException(mortgageId);
        }

        var payment = MapPaymentDtoToPayment(paymentDto);

        if (schedulePayment.Id != payment.ScheduledPaymentId)
        {
            throw new ScheduledPaymentNotFoundException(mortgageId);
        }

        await _paymentRepository.AddPaymentAsync(payment);

        await _scheduleRepository.MarkSchedulePaymentAsPaidAsync(schedulePayment.Id);
    }

    private Payment MapPaymentDtoToPayment(PaymentDto paymentDto)
    {
        var payment = new Payment();
        payment.Id = new Guid();
        payment.PaymentDate = DateTime.Now;
        payment.ScheduledPaymentId = paymentDto.ScheduledPaymentId;

        return payment;
    }
}
