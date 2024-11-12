using PCFSeguroVeicular.API.Domain.Coberturas.Repositorios;
using PCFSeguroVeicular.API.Domain.Propostas.Features.NovaProposta;
using PCFSeguroVeicular.API.Domain.Propostas.Repositorios;
using PCFSeguroVeicular.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

try
{
    // Add services to the container.

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer()
        .AddSwaggerDoc()
        .AddVersioning();

    builder.Services.AddScoped<CoberturaRepository>();
    builder.Services.AddScoped<PropostaRepository>();

    builder.Services.AddScoped<NovaPropostaHandler>();

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
    Console.WriteLine(ex.Message);
    return 1;
}
