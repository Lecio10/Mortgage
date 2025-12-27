
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection_String = "Server=localhost,1433;Database=Dev;User Id=sa;Password=Wmspass123;TrustServerCertificate=True;";
builder.Services.AddDbContext<AppDbcontext>(options => options.UseSqlServer(connection_String));
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IMortgageRepository, MortgageRepository>();
builder.Services.AddScoped<IScheduleGenerator, ScheduleGenerator>();

var app = builder.Build();

MortgageEndpoints.MapMortgageEndpoints(app);

app.Run();