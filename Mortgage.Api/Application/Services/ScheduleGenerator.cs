using Microsoft.EntityFrameworkCore;

public class ScheduleGenerator : IScheduleGenerator
{
    private readonly AppDbcontext _dbContext;

    public ScheduleGenerator(AppDbcontext dbContext)
    {
        _dbContext = dbContext;
    }

    public ScheduleDto Generate(Guid mortgage_Id)
    {
        var mortgage = _dbContext.Mortgages.FirstOrDefault(a => a.id == mortgage_Id);
        
        if (mortgage == null)
        {
            throw new MortgageNotFoundException(mortgage_Id);
        }

        var schedule = new Schedule(mortgage);
        var scheduleDto = MapScheduleToScheduleDto(schedule);

        return scheduleDto;
    }

    public ScheduleDto MapScheduleToScheduleDto(Schedule schedule)
    {
        ScheduleDto scheduleDto = new ScheduleDto();
        
        scheduleDto.Id = schedule.Id;
        scheduleDto.Generation_Date = schedule.Generation_Date;
        scheduleDto.MortgageeId = schedule.MortgageeId;
        scheduleDto.Number_Of_Payments = schedule.Number_Of_Payments;
        scheduleDto.ScheduledPayments = schedule.ScheduledPayments.Select(p => new ScheduledPaymentDto
            {
                Id = p.Id,
                Numer_Raty = p.Numer_Raty,
                Data_Płatności = p.Data_Płatności,
                Wysokość_Raty = p.Wysokość_Raty,
                Kwota_Odsetek = p.Kwota_Odsetek,
                Kwota_Kapitału = p.Kwota_Kapitału,
                Pozostało_Do_Spłaty = p.Pozostało_Do_Spłaty,
                ScheduleId = p.ScheduleId
            }).ToList();
        
        return scheduleDto;
    }


    public ScheduleDto Generate(Mortgagee mortgage)
    {
        return new ScheduleDto();
    }

}