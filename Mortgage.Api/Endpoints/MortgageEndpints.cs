using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public static class MortgageEndpoints
{
    public static void MapMortgageEndpoints (this WebApplication app)
    {
        app.MapGet("/mortgages/{id}", async (Guid id, IMortgageService mortgageService) =>
        {
            try
            {
                var mortgageDto = await mortgageService.GetMortgageByIdAsync(id);
                return Results.Ok(mortgageDto);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        app.MapPost("/mortgages", (MortgageDto mortgageDto, IMortgageService mortgageService) =>
        {
            try
            {
                mortgageService.AddMortgage(mortgageDto);
                return Results.Accepted();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
        
        app.MapGet("/mortgages/{id}/schedule", async (Guid id, IScheduleService scheduleService) =>
        {
            try
            {
                var scheduleDto = await scheduleService.GetScheduleForMortgageAsync(id);
                return Results.Ok(scheduleDto);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
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