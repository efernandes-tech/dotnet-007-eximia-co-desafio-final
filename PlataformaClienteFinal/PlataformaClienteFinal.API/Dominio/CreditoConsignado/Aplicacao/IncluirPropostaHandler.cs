using CSharpFunctionalExtensions;
using PlataformaClienteFinal.API.Dominio.CreditoConsignado.Entidades;
using PlataformaClienteFinal.API.Dominio.Infraestrutura;

namespace PlataformaClienteFinal.API.Dominio.CreditoConsignado.Aplicacao;

public class IncluirPropostaHandler
{
    private readonly CreditoConsignadoRepositorio _creditoConsignadoRepositorio;

    public IncluirPropostaHandler(CreditoConsignadoRepositorio creditoConsignadoRepositorio)
    {
        _creditoConsignadoRepositorio = creditoConsignadoRepositorio;
    }

    public async Task<Result<Proposta>> Handle(IncluirPropostaCommand command, CancellationToken cancellationToken)
    {
        // TODO: Só criar proposta para cliente cadastrado? É bom pensar em Leads
        var clienteResult = await _creditoConsignadoRepositorio.BuscarCliente(cpf: command.Cliente);
        if (clienteResult.HasNoValue)
            return Result.Failure<Proposta>("Cliente inválido");

        var agenteResult = await _creditoConsignadoRepositorio.BuscarAgente(codigo: command.Agente, cancellationToken);
        if (agenteResult.HasNoValue)
            return Result.Failure<Proposta>("Agente inválido");

        // DESAFIO: Proponente não pode ter propostas abertas;
        // TODO: Melhor carregar as propostas e validar dentro da Proposta?
        var temPropostasEmAberto = await _creditoConsignadoRepositorio.ProponenteComPropostasEmAberto(command.Cliente);
        if (temPropostasEmAberto)
            return Result.Failure<Proposta>("Cliente tem propostas em aberto");

        var propostaResult = Proposta.Criar(
            clienteResult.Value,
            valorOperacao: command.Simulacao,
            agenteResult.Value);

        if (propostaResult.IsFailure)
            return Result.Failure<Proposta>(propostaResult.Error);

        var proposta = propostaResult.Value;

        await _creditoConsignadoRepositorio.Adicionar(proposta, cancellationToken);
        await _creditoConsignadoRepositorio.Save();

        return Result.Success(proposta);
    }
}
