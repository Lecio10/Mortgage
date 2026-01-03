using Microsoft.EntityFrameworkCore;

public class ScheduleService : IScheduleService
{
    private readonly IMortgageRepository _mortgageRepository;
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleService(IMortgageRepository mortgageRepository, IScheduleRepository scheduleRepository)
    {
        _mortgageRepository = mortgageRepository;
        _scheduleRepository = scheduleRepository;
    }
    
    public async Task<ScheduleDto> GetScheduleForMortgageAsync(Guid mortgageId)
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
        
        var scheduleDto = MapScheduleToScheduleDto(schedule);

        return scheduleDto;
    }
    
    public async Task<ScheduleDto> GenerateSchedule(Guid mortgageId)
    {
        var mortgage = await _mortgageRepository.GetMortgageByIdAsync(mortgageId);
        
        if (mortgage is null)
        {
            throw new MortgageNotFoundException(mortgageId);
        }
        var scheduleGenerator = new ScheduleGenerator();
        var schedule = scheduleGenerator.Generate(mortgage, null);

        if (schedule is null)
        {
            throw new ScheduleGenerationException(mortgageId);
        }
        
        await _scheduleRepository.SaveScheduleAsync(schedule);

        var scheduleDto = MapScheduleToScheduleDto(schedule);

        return scheduleDto;
    }

    public ScheduleDto MapScheduleToScheduleDto(Schedule schedule)
    {
        ScheduleDto scheduleDto = new ScheduleDto();
        var index = 1;
        
        scheduleDto.Id = schedule.Id;
        scheduleDto.Generation_Date = schedule.Generation_Date;
        scheduleDto.MortgageeId = schedule.MortgageeId;

        scheduleDto.ScheduledPayments = schedule.ScheduledPayments
        .Where(s => s.IsPaid == false)
        .OrderBy(p => p.Numer_Raty)
        .Select(p => new ScheduledPaymentDto
            {
                Id = p.Id,
                Numer_Raty = index++,
                Data_Płatności = p.Data_Płatności,
                Wysokość_Raty = p.Wysokość_Raty,
                Kwota_Odsetek = p.Kwota_Odsetek,
                Kwota_Kapitału = p.Kwota_Kapitału,
                Pozostało_Do_Spłaty = p.Pozostało_Do_Spłaty
            }).ToList();

        scheduleDto.Number_Of_Payments = scheduleDto.ScheduledPayments.Count;

        return scheduleDto;
    }
}