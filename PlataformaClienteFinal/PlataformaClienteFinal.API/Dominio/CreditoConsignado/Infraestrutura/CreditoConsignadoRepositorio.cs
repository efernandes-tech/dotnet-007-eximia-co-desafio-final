using CSharpFunctionalExtensions;
using Dapper;
using Microsoft.EntityFrameworkCore;
using PlataformaClienteFinal.API.Comum;
using PlataformaClienteFinal.API.Dominio.CreditoConsignado.Entidades;

namespace PlataformaClienteFinal.API.Dominio.Infraestrutura;

public sealed class CreditoConsignadoRepositorio(
    PlataformaClienteFinalDbContext dbContext,
    ILogger<CreditoConsignadoRepositorio> logger
    ) : IService<CreditoConsignadoRepositorio>
{
    public async Task<Maybe<Cliente>> BuscarCliente(string cpf)
    {
        return (await dbContext.Clientes.FirstOrDefaultAsync(c => c.Cpf == cpf))
            ?? Maybe<Cliente>.None;
    }

    public async Task<Maybe<Agente>> BuscarAgente(int codigo, CancellationToken cancellationToken)
    {
        var agente = await dbContext.Agentes.FirstOrDefaultAsync(c => c.Codigo == codigo, cancellationToken);

        if (agente is null)
            logger.LogWarning("Agente não foi localizado no banco de dados");

        return agente ?? Maybe<Agente>.None;
    }

    public async Task Adicionar(Proposta propostaCreditoConsignado, CancellationToken cancellationToken)
    {
        await dbContext.PropostaCreditoConsignados.AddAsync(propostaCreditoConsignado, cancellationToken);
    }

    public Task Save()
    {
        return dbContext.SaveChangesAsync();
    }

    public async Task<bool> ProponenteComPropostasEmAberto(string clienteCpf)
    {
        var result = await dbContext.Database.GetDbConnection()
            .QueryFirstOrDefaultAsync<string>("SELECT cliente " +
                "FROM Propostas " +
                "WHERE cliente = @clienteCpf" +
                "   AND situacao = @situacao",
                new { clienteCpf, EPropostaSituacao.EmAnalise });

        return result == clienteCpf;
    }
}
