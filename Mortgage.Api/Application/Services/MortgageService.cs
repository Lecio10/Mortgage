using Microsoft.Identity.Client;

public class MortgageService : IMortgageService
{
    private readonly IMortgageRepository _mortgageRepository;
    private readonly IScheduleRepository _scheduleRepository;

    public MortgageService(IMortgageRepository mortgageRepository, IScheduleRepository scheduleRepository)
    {
        _mortgageRepository = mortgageRepository;
        _scheduleRepository = scheduleRepository;
    }
    
    public async Task<MortgageDetailsDto> GetMortgageByIdAsync(Guid mortgageId)
    {
        var mortgage = await _mortgageRepository.GetMortgageByIdAsync(mortgageId);

        if (mortgage is null)
        {
            throw new MortgageNotFoundException(mortgageId);
        }

        var mortgageDetailsDto = MapMortgageToMortgageDetailsDto(mortgage);

        return mortgageDetailsDto;
    }

    public async Task AddMortgageAsync(MortgageDto mortgageDto)
    {
        var mortgage = MapMortgageDtoToMortgage(mortgageDto);
        await _mortgageRepository.AddMortgageAsync(mortgage);
        
        var schedule = new ScheduleGenerator().Generate(mortgage, null);
        await _scheduleRepository.SaveScheduleAsync(schedule);

        await _mortgageRepository.UpdatePostScheduleAsync(mortgage, schedule);
    }

    private MortgageDetailsDto MapMortgageToMortgageDetailsDto(Mortgagee mortgage)
    {
        var mortgageDetailsDto = new MortgageDetailsDto();
        
        mortgageDetailsDto.Loan_Amount = mortgage.Loan_Ammount;
        mortgageDetailsDto.Interest_Rate_In_Percent = mortgage.Interest_Rate_In_Percent;
        mortgageDetailsDto.Number_Of_Instalments = mortgage.Number_Of_Instalments; 
        mortgageDetailsDto.Next_Instalment_Date = mortgage.Next_Instalment_Date;
        mortgageDetailsDto.Remaining_Loan = mortgage.Remainig_Loan;
        mortgageDetailsDto.Remaining_Instalments = mortgage.Remaining_Instalments;
        mortgageDetailsDto.Interest_Sum = mortgage.Schedule_Based_Interest_Sum;
        mortgageDetailsDto.Total_Sum = mortgage.Schedule_Based_Total_Sum;

        return mortgageDetailsDto;
    }

    private Mortgagee MapMortgageDtoToMortgage(MortgageDto mortgageDto)
    {
        var mortgage = new Mortgagee();

        mortgage.First_Instalment_Date = mortgageDto.First_Instalment_Date;
        mortgage.Next_Instalment_Date = mortgageDto.First_Instalment_Date;
        mortgage.Loan_Ammount = mortgageDto.Loan_Ammount;
        mortgage.Remainig_Loan = Convert.ToDecimal(mortgageDto.Loan_Ammount);
        mortgage.Number_Of_Instalments = mortgageDto.Instalments;
        mortgage.Remaining_Instalments = Convert.ToInt32(mortgageDto.Instalments);
        mortgage.Interest_Rate_In_Percent = mortgageDto.Interest_Rate_In_Percent;

        mortgage.Schedule_Based_Interest_Sum = 0;
        mortgage.Schedule_Based_Total_Sum = 0;

        return mortgage;
    }
}