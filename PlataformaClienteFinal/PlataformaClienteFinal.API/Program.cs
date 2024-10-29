using Microsoft.EntityFrameworkCore;
using PlataformaClienteFinal.API.Dominio;
using PlataformaClienteFinal.API.Dominio.CreditoConsignado.Aplicacao;
using PlataformaClienteFinal.API.Dominio.Infraestrutura;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var assemblyName = Assembly.GetExecutingAssembly().GetName();
var serviceName = assemblyName.Name;

// Configure Serilog as the logging provider
Log.Logger = new LoggerConfiguration()
    .Enrich.WithProperty("ApplicationName", serviceName)
    .WriteTo.Console() // Output logs to the console; you can add other sinks as needed
    .CreateLogger();

builder.Host.UseSerilog(); // Add Serilog to the DI system

try
{
    Log.Information("Starting application");

    // Add services to the container.
    builder.Services.AddDbContext<PlataformaClienteFinalDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("PlataformaClienteFinalDBConnection")));

    builder.Services.AddScoped<CreditoConsignadoRepositorio>();
    builder.Services.AddScoped<IncluirPropostaHandler>();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Program terminated unexpectedly");

    return 1;
}
finally
{
    Log.CloseAndFlush();
}
