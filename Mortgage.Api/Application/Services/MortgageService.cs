public class MortgageService : IMortgageService
{
    private readonly IMortgageRepository _mortgageRepository;

    public MortgageService(IMortgageRepository mortgageRepository)
    {
        _mortgageRepository = mortgageRepository;
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

    public async Task AddMortgage(MortgageDto mortgageDto)
    {
        var mortgage = MapMortgageDtoToMortgage(mortgageDto);
        await _mortgageRepository.AddMortgage(mortgage);
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