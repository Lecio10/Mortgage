public class MortgageService : IMortgageService
{
    private readonly IMortgageRepository _mortgageRepository;
    private readonly IScheduleRepository _scheduleRepository;

    public MortgageService(IMortgageRepository mortgageRepository, IScheduleRepository scheduleRepository)
    {
        _mortgageRepository = mortgageRepository;
        _scheduleRepository = scheduleRepository;
    }
    
    public async Task<MortgageDto> GetMortgageByIdAsync(Guid mortgageId)
    {
        var mortgage = await _mortgageRepository.GetMortgageByIdAsync(mortgageId);

        if (mortgage is null)
        {
            throw new MortgageNotFoundException(mortgageId);
        }

        var mortgageDto = MapMortgageToMortgageDto(mortgage);

        return mortgageDto;
    }

    public async Task AddMortgageAsync(MortgageDto mortgageDto)
    {
        var mortgage = MapMortgageDtoToMortgage(mortgageDto);
        await _mortgageRepository.AddMortgageAsync(mortgage);
        
        var schedule = new ScheduleGenerator().Generate(mortgage, null);
        await _scheduleRepository.SaveScheduleAsync(schedule);
    }

    private MortgageDto MapMortgageToMortgageDto(Mortgagee mortgage)
    {
        var mortgageDto = new MortgageDto();
        
        mortgageDto.First_Instalment_Date = mortgage.First_Instalment_Date;
        mortgageDto.Loan_Ammount = mortgage.Loan_Ammount;
        mortgageDto.Instalments = mortgage.Instalments;
        mortgageDto.Interest_Rate_In_Percent = mortgage.Interest_Rate_In_Percent;

        return mortgageDto;
    }

    private Mortgagee MapMortgageDtoToMortgage(MortgageDto mortgageDto)
    {
        var mortgage = new Mortgagee();

        mortgage.First_Instalment_Date = mortgageDto.First_Instalment_Date;
        mortgage.Loan_Ammount = mortgageDto.Loan_Ammount;
        mortgage.Instalments = mortgageDto.Instalments;
        mortgage.Interest_Rate_In_Percent = mortgageDto.Interest_Rate_In_Percent;

        return mortgage;
    }
}