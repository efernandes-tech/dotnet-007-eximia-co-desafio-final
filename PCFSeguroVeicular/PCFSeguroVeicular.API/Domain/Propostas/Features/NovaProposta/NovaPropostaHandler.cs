using CSharpFunctionalExtensions;
using PCFSeguroVeicular.API.Domain.Coberturas.Entidades;
using PCFSeguroVeicular.API.Domain.Coberturas.Repositorios;
using PCFSeguroVeicular.API.Domain.Propostas.Entidades;
using PCFSeguroVeicular.API.Domain.Propostas.Repositorios;

namespace PCFSeguroVeicular.API.Domain.Propostas.Features.NovaProposta;

public class NovaPropostaHandler(CoberturaRepository coberturaRepository,
    PropostaRepository propostaRepository)
{
    public async Task<Result<int>> ExecutarAsync(
        NovaPropostaCommand novaPropostaCommand,
        CancellationToken cancellationToken)
    {
        // Validar as coberturas
        if (!novaPropostaCommand.coberturas.Any())
        {
            return Result.Failure<int>("Coberturas não informadas.");
        }

        var coberturas = new List<Maybe<Cobertura>>();
        foreach (var cobertura in novaPropostaCommand.coberturas)
        {
            var coberturaDb = await coberturaRepository.ObterCoberturaPorCodigoAsync(cobertura.Codigo);
            if (coberturaDb.HasNoValue)
            {
                return Result.Failure<int>("Cobertura inválida.");
            }
            coberturas.Add(coberturaDb);
        }

        // Consultar tabela FIPE

        // Consultar historico de acidentes

        // Calcular nivel de risco

        // Calcular valor do seguro
        var valorSeguro = 1000;

        // Criar a proposta de seguro
        var id = 1;

        var proposta = Proposta.Criar(id, valorSeguro);

        if (proposta.IsFailure)
            return Result.Failure<int>(proposta.Error);

        await propostaRepository.Adicionar(proposta.Value, cancellationToken);

        //await unitOfWork.CommitAsync(cancellationToken);

        // Iniciar workflow de aprovacao

        return Result.Success(1);
    }
}
