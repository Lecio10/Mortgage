using System.Globalization;
using Microsoft.Identity.Client;

public class OverpaymentService : IOverpaymentService
{
    private readonly IOverpaymentRepository _overpaymentRepository;
    private readonly IMortgageRepository _mortgageRepository;
    private readonly IScheduleRepository _scheduleRepository;
    public OverpaymentService(IOverpaymentRepository overpaymentRepository, IMortgageRepository mortgageRepository, IScheduleRepository scheduleRepository)
    {
        _overpaymentRepository = overpaymentRepository;
        _mortgageRepository = mortgageRepository;
        _scheduleRepository = scheduleRepository;
    }

    public async Task<List<OverpaymentDto>?> GetOverpayentsForMortgageAsync(Guid mortgageId)
    {
        var mortgage = await _mortgageRepository.GetMortgageByIdAsync(mortgageId);
        
        if (mortgage is null)
        {
            throw new MortgageNotFoundException(mortgageId);
        }

        var overpayments = await _overpaymentRepository.GetOverpaymentsForMortgageAsync(mortgageId);

        var overpaymentDtos = MapOverpaymentsToOverpaymentDtos(overpayments);

        return overpaymentDtos;
    }
    
    public async Task AddOverpaymentAsync(OverpaymentDto overpaymentDto, Guid mortgageId)
    {
        var mortgage = await _mortgageRepository.GetMortgageByIdAsync(mortgageId);
        
        if (mortgage is null)
        {
            throw new MortgageNotFoundException(mortgageId);
        }
        
        var overpayment = MapOverpaymentDtoToOverpayment(overpaymentDto, mortgageId);
        
        if (overpayment is null)
        {
            throw new ArgumentNullException();
        }
        
        await _overpaymentRepository.AddOverpaymentAsync(overpayment);

        var overpayments = await _overpaymentRepository.GetOverpaymentsForMortgageAsync(mortgageId);
        
        var schedule = new ScheduleGenerator().Generate(mortgage, overpayments);
        
        if (schedule is null)
        {
            throw new ScheduleGenerationException(mortgageId);
        }
        
        await _scheduleRepository.SaveScheduleAsync(schedule);
    }

    private Overpayment? MapOverpaymentDtoToOverpayment(OverpaymentDto overpaymentDto, Guid mortgageId)
    {
        if (!DateTime.TryParseExact(overpaymentDto.Overpayment_Date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
        {
            return null;
        }

        var overpayment = new Overpayment();

        overpayment.Id = new Guid();
        overpayment.Overpayment_Date = parsedDate;
        overpayment.Amount = overpaymentDto.Amount;
        overpayment.MortgageeId = mortgageId;

        return overpayment;
    }

    private List<OverpaymentDto>? MapOverpaymentsToOverpaymentDtos(List<Overpayment> overpayments)
    {
        var overpaymentDtos = new List<OverpaymentDto>();

        overpaymentDtos = overpayments
        .OrderBy(p => p.Overpayment_Date)
        .Select(p => new OverpaymentDto
        {
            Overpayment_Date = p.Overpayment_Date.ToString("yyyy-MM-dd"),
            Amount = p.Amount
        }).ToList();

        return overpaymentDtos;
    }
}