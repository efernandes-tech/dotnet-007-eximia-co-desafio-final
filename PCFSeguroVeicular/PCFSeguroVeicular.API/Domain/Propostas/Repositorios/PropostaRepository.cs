using PCFSeguroVeicular.API.Domain.Infra;
using PCFSeguroVeicular.API.Domain.Propostas.Entidades;

namespace PCFSeguroVeicular.API.Domain.Propostas.Repositorios;

public sealed class PropostaRepository(PCFSeguroVeicularDbContext context)
{
    public async Task Adicionar(Proposta proposta, CancellationToken cancellationToken)
    {
        await context.Propostas.AddAsync(proposta, cancellationToken);
    }
}
