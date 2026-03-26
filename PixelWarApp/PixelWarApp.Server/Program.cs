using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using PixelWarApp.Server.Data;
using PixelWarApp.Server.Services;

var builder = WebApplication.CreateBuilder(args);

//surcharger avec les env vars K3s
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<PixelDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PixelsDB")));


builder.Services.AddScoped<PixelService>();

// Heatlh checks
builder.Services.AddHealthChecks()
        .AddNpgSql(builder.Configuration.GetConnectionString("PixelsDB"));

// OpenTelemetry + Prometheus
builder.Services.AddOpenTelemetry()
        .WithMetrics(metrics =>
        {
            metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddPrometheusExporter();
        });


builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();


app.UseCors("ReactApp");
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Health check endpoint
app.MapHealthChecks("/health");

// Prometheus metrics endpoint
app.MapPrometheusScrapingEndpoint();

app.MapFallbackToFile("/index.html");

// Migration automatique
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PixelDbContext>();
    db.Database.Migrate();
}

app.Run();
