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
                var mortgageDetailsDto = await mortgageService.GetMortgageByIdAsync(id);
                return Results.Ok(mortgageDetailsDto);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        app.MapPost("/mortgages", async (MortgageDto mortgageDto, IMortgageService mortgageService) =>
        {
            try
            {
                await mortgageService.AddMortgageAsync(mortgageDto);
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
        
        app.MapPost("/mortgages/{id}/overpayments", async (OverpaymentDto overpaymentDto, Guid id, IOverpaymentService overpaymentService) =>
        {
            try
            {
                await overpaymentService.AddOverpaymentAsync(overpaymentDto, id);
                return Results.Accepted();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        app.MapGet("/mortgages/{id}/overpayments", async (Guid id, IOverpaymentService overpaymentService) =>
        {
            try
            {
                var overpaymentDtos = await overpaymentService.GetOverpayentsForMortgageAsync(id);
                return Results.Ok(overpaymentDtos);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        app.MapPost("/mortgages/{id}/payment", async (Guid id,  PaymentDto paymentDto, IPaymentService paymentService) =>
        {
            try
            {
                await paymentService.ProcessPaymentAsync(id, paymentDto);
                return Results.Accepted();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        // app.MapGet("/mortgages/{id}/shorten", async (Guid id, IOverpaymentService overpaymentService) =>
        // {
        //     try
        //     {
        //         var overpaymentDtos = await overpaymentService.GetOverpayentsForMortgageAsync(id);
        //         return Results.Ok(overpaymentDtos);
        //     }
        //     catch (Exception ex)
        //     {
        //         return Results.BadRequest(ex.Message);
        //     }
        // });

        // app.MapPost("/mortgages/{id}/shorten", async (OverpaymentDto overpaymentDto, Guid id, IOverpaymentService overpaymentService) =>
        // {
        //     try
        //     {
        //         await overpaymentService.AddOverpaymentAsync(overpaymentDto, id);
        //         return Results.Accepted();
        //     }
        //     catch (Exception ex)
        //     {
        //         return Results.BadRequest(ex.Message);
        //     }
        // });
    }
}