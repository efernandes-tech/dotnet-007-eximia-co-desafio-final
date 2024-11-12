using CSharpFunctionalExtensions;
using PCFSeguroVeicular.API.Domain.Coberturas.Entidades;
using PCFSeguroVeicular.API.Domain.Infra;

namespace PCFSeguroVeicular.API.Domain.Coberturas.Repositorios;

public sealed class CoberturaRepository(PCFSeguroVeicularDbContext context)
{
    public async Task<Maybe<Cobertura>> ObterCoberturaPorCodigoAsync(string codigo, CancellationToken cancellationToken = default)
    {
        var cobertura = await context.Coberturas
            .FindAsync(codigo, cancellationToken)
            .ConfigureAwait(false);

        if (cobertura is null)
            return Maybe.None;

        return cobertura;
    }
}
