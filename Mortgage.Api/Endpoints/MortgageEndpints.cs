using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public static class MortgageEndpoints
{
    public static void MapMortgageEndpoints (this WebApplication app)
    {
        app.MapPost("/mortgages", (MortgageDto mortgageDto, AppDbcontext dbcontext) =>
        {
            Mortgagee mortgage = new Mortgagee();
            mortgage.Loan_Ammount = mortgageDto.Loan_Ammount;
            mortgage.First_Instalment_Date = mortgageDto.First_Instalment_Date;
            mortgage.Instalments = mortgageDto.Instalments;
            mortgage.Interest_Rate_In_Percent = mortgageDto.Interest_Rate_In_Percent;

            dbcontext.Add(mortgage);
            dbcontext.SaveChanges();

            IScheduleService scheduleService = new ScheduleService(dbcontext);
            var schedule = scheduleService.GenerateSchedule(mortgage.id);

            return Results.Accepted();
        });

        app.MapGet("/mortgages/{id}", (Guid id, AppDbcontext dbcontext) =>
        {
            var mortgage = dbcontext.Mortgages.FirstOrDefault(m => m.id == id);
            
            return mortgage is not null ? Results.Ok(mortgage) : Results.NotFound();

        });
        
        app.MapGet("/mortgages/{id}/schedule", (Guid id, AppDbcontext dbcontext) =>
        {
            IScheduleService scheduleService = new ScheduleService(dbcontext);
            var scheduleDto = scheduleService.GenerateSchedule(id);

            return Results.Json(scheduleDto);
        });
        
        app.MapPost("/mortgages/{id}/overpayments", (OverpaymentDto dto, Guid id, AppDbcontext dbcontext) =>
        {
            var overpayments = new List<Overpayment> { new Overpayment
            {
                Amount = dto.Amount,
                Overpayment_Date = DateTime.Parse(dto.Overpayment_Date)
            }};

            return Results.Ok($"Nadpłata zarejestrowana");
        });

        app.MapGet("/mortgages/{id}/overpayments", (Guid id, AppDbcontext dbcontext) =>
        {
            var overpayments = new List<Overpayment>();
            return Results.Json(overpayments);
        });
    }
}