using a_webapi.BackgroundServices;
using a_webapi.Configuration;
using a_webapi.Data;
using a_webapi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host = localhost;Port=5432;Database=networkmonitor;Username=postgres;Password=mojeheslo;"));

builder.Services.AddScoped<DeviceService>();
builder.Services.AddScoped<DeviceMonitoringService>();
builder.Services.AddHostedService<DeviceMonitoringBackgroundService>();

builder.Services.Configure<MonitoringOptions>(builder.Configuration.GetSection("Monitoring"));

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler("/Error");
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();